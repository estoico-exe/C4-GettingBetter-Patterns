using Structurizr;
using Structurizr.Api;

namespace c4_model_design
{
    class Program
    {
        static void Main(string[] args)
        {
            Banking();
        }

        static void Banking()
        {
            const long workspaceId = 70012;
            const string apiKey = "48acaf5f-5494-443f-9827-677dd79528ec";
            const string apiSecret = "fdcd0bbe-e4e5-4234-85ca-e80c9db0cf2e";

            StructurizrClient structurizrClient = new StructurizrClient(apiKey, apiSecret);
            Workspace workspace = new Workspace("Software Design & Patterns - C4 Model - GettingBetter", "Plataforma de asesría de e-sports GettingBetter");
            ViewSet viewSet = workspace.Views;
            Model model = workspace.Model;

            // 1. Diagrama de Contexto
            SoftwareSystem gettingBetter = model.AddSoftwareSystem("Getting Better", "Permite conectar coaches profesionales con aficionados a e-sports y dueños de centros gaming para la organización de torneos.");
            SoftwareSystem trackerGg = model.AddSoftwareSystem("TrackerGG", "Plataforma que ofrece una REST API de información de estadisticas de un jugador.");
            SoftwareSystem paypalApi = model.AddSoftwareSystem("PayPal API", "API de la empresa PayPal que permite pagos de manera online.");

            Person coach = model.AddPerson("Coach", "Jugador profesional de juegos electronicos que quiera asesoraras a aficionados en su técnica de juego.");
            Person jugador = model.AddPerson("Jugador", "Aficionado de juegos electronicos que busca mejorar su técnica de juego.");
            Person cyber = model.AddPerson("Cyber", "Dueño de un centro gaming que quiera participar en torneos de juegos electronicos.");
            
            coach.Uses(gettingBetter, "Usa la plataforma para brindar asesorías y organizar torneos.");
            jugador.Uses(gettingBetter, "Usa la plataforma para recibir asesorías y participar en torneos.");
            cyber.Uses(gettingBetter, "Usa la plataforma para formar parte de la organización de torneos gaming.");
            gettingBetter.Uses(trackerGg, "Consulta información de estadisiticas de jugadores");
            gettingBetter.Uses(paypalApi, "Usa la API de PayPal.");
            
            SystemContextView contextView = viewSet.CreateSystemContextView(gettingBetter, "Contexto", "Diagrama de contexto");
            contextView.PaperSize = PaperSize.A4_Landscape;
            contextView.AddAllSoftwareSystems();
            contextView.AddAllPeople();

            // Tags
            coach.AddTags("Coach");
            jugador.AddTags("Jugador");
            cyber.AddTags("Cyber");
            gettingBetter.AddTags("GettingBetter");
            trackerGg.AddTags("TrackerGG");
            paypalApi.AddTags("PayPalAPI");

            Styles styles = viewSet.Configuration.Styles;
            styles.Add(new ElementStyle("Coach") { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("Jugador") { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("Cyber") { Background = "#0a60ff", Color = "#ffffff", Shape = Shape.Person });
            styles.Add(new ElementStyle("GettingBetter") { Background = "#008f39", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("TrackerGG") { Background = "#90714c", Color = "#ffffff", Shape = Shape.RoundedBox });
            styles.Add(new ElementStyle("PayPalAPI") { Background = "#2f95c7", Color = "#ffffff", Shape = Shape.RoundedBox });

            // 2. Diagrama de Contenedores
            Container mobileApplication = gettingBetter.AddContainer("Mobile App", "Permite a los usuarios visualizar un dashboard con el resumen de toda la información de su desempeño en el juego.", "Flutter");
            Container webApplication = gettingBetter.AddContainer("Web App", "Permite a los usuarios visualizar un dashboard con el resumen de toda la infomarción de su desempeño en el juego.", "Flutter Web");
            Container landingPage = gettingBetter.AddContainer("Landing Page", "", "Flutter Web");
            Container apiRest = gettingBetter.AddContainer("API Rest", "API Rest", "NodeJS (NestJS) port 8080");
            Container userContext = gettingBetter.AddContainer("User Context", "Bounded Context del Microservicio de registro de los usuarios en la plataforma.", "NodeJS (NestJS)");
            Container clasesContext = gettingBetter.AddContainer("Clases Context", "Bounded Context del Microservicio de asesorias.", "NodeJS (NestJS)");
            Container paymentContext = gettingBetter.AddContainer("Payment Context", "Bounded Context del Microservicio de pasarela de pagos.", "NodeJS (NestJS)");
            Container reservationContext = gettingBetter.AddContainer("Reservation Context", "Bounded Context del Microservicio de reserva de asesorias.", "NodeJS (NestJS)");
            Container offersContext = gettingBetter.AddContainer("Offers Context", "Bounded Context del Microservicio de ofertas por parte de los coaches para con los jugadores.", "NodeJS (NestJS)");
            Container database = gettingBetter.AddContainer("Database", "", "Oracle");
            
            coach.Uses(mobileApplication, "Consulta");
            coach.Uses(webApplication, "Consulta");
            coach.Uses(landingPage, "Consulta");
            jugador.Uses(mobileApplication, "Consulta");
            jugador.Uses(webApplication, "Consulta");
            jugador.Uses(landingPage, "Consulta");
            cyber.Uses(mobileApplication, "Consulta");
            cyber.Uses(webApplication, "Consulta");
            cyber.Uses(landingPage, "Consulta");

            mobileApplication.Uses(apiRest, "API Request", "JSON/HTTPS");
            webApplication.Uses(apiRest, "API Request", "JSON/HTTPS");

            apiRest.Uses(userContext, "", "");
            apiRest.Uses(clasesContext, "", "");
            apiRest.Uses(paymentContext, "", "");
            apiRest.Uses(reservationContext, "", "");
            apiRest.Uses(offersContext, "", "");
            
            userContext.Uses(database, "", "JDBC");
            clasesContext.Uses(database, "", "JDBC");
            paymentContext.Uses(database, "", "JDBC");
            reservationContext.Uses(database, "", "JDBC");
            offersContext.Uses(database, "", "JDBC");
            
            clasesContext.Uses(trackerGg, "API Request", "JSON/HTTPS");
            paymentContext.Uses(paypalApi, "API Request", "JSON/HTTPS");

            // Tags
            mobileApplication.AddTags("MobileApp");
            webApplication.AddTags("WebApp");
            landingPage.AddTags("LandingPage");
            apiRest.AddTags("APIRest");
            database.AddTags("Database");
            userContext.AddTags("FlightPlanningContext");
            clasesContext.AddTags("AirportContext");
            paymentContext.AddTags("AircraftInventoryContext");
            reservationContext.AddTags("VaccinesInventoryContext");
            offersContext.AddTags("MonitoringContext");

            styles.Add(new ElementStyle("MobileApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.MobileDevicePortrait, Icon = "" });
            styles.Add(new ElementStyle("WebApp") { Background = "#9d33d6", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("LandingPage") { Background = "#929000", Color = "#ffffff", Shape = Shape.WebBrowser, Icon = "" });
            styles.Add(new ElementStyle("APIRest") { Shape = Shape.RoundedBox, Background = "#0000ff", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle("Database") { Shape = Shape.Cylinder, Background = "#ff0000", Color = "#ffffff", Icon = "" });
            styles.Add(new ElementStyle("UserContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("ClasesContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("PaymentContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("ReservationContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("OffersContext") { Shape = Shape.Hexagon, Background = "#facc2e", Icon = "" });

            ContainerView containerView = viewSet.CreateContainerView(gettingBetter, "Contenedor", "Diagrama de contenedores");
            contextView.PaperSize = PaperSize.A4_Landscape;
            containerView.AddAllElements();

            // 3. Diagrama de Componentes
            /*
            Component domainLayer = monitoringContext.AddComponent("Domain Layer", "", "NodeJS (NestJS)");
            Component monitoringController = monitoringContext.AddComponent("Monitoring Controller", "REST API endpoints de monitoreo.", "NodeJS (NestJS) REST Controller");
            Component monitoringApplicationService = monitoringContext.AddComponent("Monitoring Application Service", "Provee métodos para el monitoreo, pertenece a la capa Application de DDD", "NestJS Component");
            Component flightRepository = monitoringContext.AddComponent("Flight Repository", "Información del vuelo", "NestJS Component");
            Component vaccineLoteRepository = monitoringContext.AddComponent("VaccineLote Repository", "Información de lote de vacunas", "NestJS Component");
            Component locationRepository = monitoringContext.AddComponent("Location Repository", "Ubicación del vuelo", "NestJS Component");
            Component aircraftSystemFacade = monitoringContext.AddComponent("Aircraft System Facade", "", "NestJS Component");

            apiRest.Uses(monitoringController, "", "JSON/HTTPS");
            monitoringController.Uses(monitoringApplicationService, "Invoca métodos de monitoreo");
            monitoringController.Uses(aircraftSystemFacade, "Usa");
            monitoringApplicationService.Uses(domainLayer, "Usa", "");
            monitoringApplicationService.Uses(flightRepository, "", "JDBC");
            monitoringApplicationService.Uses(vaccineLoteRepository, "", "JDBC");
            monitoringApplicationService.Uses(locationRepository, "", "JDBC");
            flightRepository.Uses(database, "", "JDBC");
            vaccineLoteRepository.Uses(database, "", "JDBC");
            locationRepository.Uses(database, "", "JDBC");
            locationRepository.Uses(googleMaps, "", "JSON/HTTPS");
            aircraftSystemFacade.Uses(aircraftSystem, "JSON/HTTPS");
            
            // Tags
            domainLayer.AddTags("DomainLayer");
            monitoringController.AddTags("MonitoringController");
            monitoringApplicationService.AddTags("MonitoringApplicationService");
            flightRepository.AddTags("FlightRepository");
            vaccineLoteRepository.AddTags("VaccineLoteRepository");
            locationRepository.AddTags("LocationRepository");
            aircraftSystemFacade.AddTags("AircraftSystemFacade");
            
            styles.Add(new ElementStyle("DomainLayer") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MonitoringController") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MonitoringApplicationService") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("MonitoringDomainModel") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("FlightStatus") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("FlightRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("VaccineLoteRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("LocationRepository") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });
            styles.Add(new ElementStyle("AircraftSystemFacade") { Shape = Shape.Component, Background = "#facc2e", Icon = "" });

            ComponentView componentView = viewSet.CreateComponentView(monitoringContext, "Components", "Component Diagram");
            componentView.PaperSize = PaperSize.A4_Landscape;
            componentView.Add(mobileApplication);
            componentView.Add(webApplication);
            componentView.Add(apiRest);
            componentView.Add(database);
            componentView.Add(aircraftSystem);
            componentView.Add(googleMaps);
            componentView.AddAllComponents();
            */

            structurizrClient.UnlockWorkspace(workspaceId);
            structurizrClient.PutWorkspace(workspaceId, workspace);
        }
    }
}