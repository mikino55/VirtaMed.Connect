using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using VirtaMed.Data.Entity;
using VirtaMed.Data.Shared.DTO.Deployment;
using VirtaMed.Persistence;
using VirtaMed.Persistence.Repository;
using VirtaMed.Services.Mapping;

namespace VirtaMed.Connect.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeploymentController : ControllerBase
    {
        private readonly IDataMapper mapper;
        private readonly SimulatorFamilyRepository simulatorFamilyRepository;
        private readonly SceneRepository sceneRepository;
        private readonly ILogger<DeploymentController> logger;
        private readonly MongoClientWrapper clientWrapper;
        private readonly SimulatorLocalizationRepository localizationRepository;

        public DeploymentController(IDataMapper mapper,
                                    SimulatorFamilyRepository simulatorFamilyRepository,
                                    SceneRepository sceneRepository,
                                    ILogger<DeploymentController> logger,
                                    MongoClientWrapper clientWrapper,
                                    SimulatorLocalizationRepository localizationRepository)
        {
            this.mapper = mapper;
            this.simulatorFamilyRepository = simulatorFamilyRepository;
            this.sceneRepository = sceneRepository;
            this.logger = logger;
            this.clientWrapper = clientWrapper;
            this.localizationRepository = localizationRepository;
        }

        [Route("localization")]
        [HttpPost]
        public async Task<IActionResult> DeployLocalization(LocalizationDto localizationDto)
        {
            bool simulatorExists = await this.simulatorFamilyRepository.SimulatorExists(localizationDto.SimulatorIdentifier, localizationDto.SimulatorVersion);
            if (!simulatorExists)
            {
                return NotFound($"Simulator(Identifier='{localizationDto.SimulatorIdentifier}', Version='{localizationDto.SimulatorVersion}' not found.");
            }

            bool localizationExists = await this.localizationRepository.Exists(localizationDto.SimulatorIdentifier,
                                                                               localizationDto.SimulatorVersion,
                                                                               localizationDto.Dictionaries.Select(x => x.Culture));
            if (localizationExists)
            {
                return Conflict($"Localization for Simulator(Identifier='{localizationDto.SimulatorIdentifier}', Version='{localizationDto.SimulatorVersion}' and cultures '{string.Join(",", localizationDto.Dictionaries.Select(x => x.Culture))}' already exists");
            }
            var localization = this.mapper.Map<SimulatorLocalization>(localizationDto);
            await localizationRepository.InsertOne(localization);
            return Ok();
        }

        [Route("simulator")]
        [HttpPost]
        public async Task<IActionResult> DeploySimulator(SimulatorDto simulatorDto)
        {
            logger.LogInformation("Deploying simulator...");
            bool simulatorExists = await this.simulatorFamilyRepository.SimulatorExists(simulatorDto.Identifier, simulatorDto.Version);
            if (simulatorExists)
            {
                return Conflict($"Simulator(Identifier='{simulatorDto.Identifier}', Version={simulatorDto.Version}, NameLanguageKey='{simulatorDto.NameLanguageKey}' already exists.");
            }

            SimulatorFamily family = new SimulatorFamily
            {
                Identifier = simulatorDto.SimulatorFamilyIdentifier,
                NameLanguageKey = simulatorDto.SimulatorFamilyNameLanguageKey
            };

            Simulator simulator = this.mapper.Map<Simulator>(simulatorDto);
            List<Scene> scenes = this.mapper.Map<IEnumerable<Scene>>(simulatorDto.Sections.SelectMany(x => x.Scenes)).ToList();
            scenes.ForEach(scene => { scene.SimulatorIdentifier = simulatorDto.Identifier; scene.SimulatorVersion = simulatorDto.Version; });

            using (IClientSessionHandle session = await this.clientWrapper.Client.StartSessionAsync(new ClientSessionOptions()))
            {
                try
                {
                    session.StartTransaction();
                    await this.sceneRepository.InsertMany(scenes, session);
                    foreach (var sectionDto in simulatorDto.Sections)
                    {
                        var section = simulator.SceneSections.FirstOrDefault(x => x.Identifier == sectionDto.Identifier);
                        foreach (var sceneOfSection in sectionDto.Scenes)
                        {
                            section.SceneIdentifiers.Add(scenes.FirstOrDefault(x => x.Identifier == sceneOfSection.Identifier).Id);
                        }
                    }

                    await this.simulatorFamilyRepository.UpsertAndAddSimulator(family, simulator, session);
                    await session.CommitTransactionAsync();
                }
                catch (Exception)
                {
                    await session.AbortTransactionAsync();
                    throw;
                }
            }
            
            
            logger.LogInformation("Deploying simulator finished");
            return Ok();
        }

        private void kokot(Scene obj)
        {
            throw new NotImplementedException();
        }
    }
}