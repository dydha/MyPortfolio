using MudBlazor;
using MyPortfolio.Models;

namespace MyPortfolio.Services
{
    public static class ProjectService
    {
        private static readonly List<Project> _projects = new()
        {
            new Project
            {
               Id = 1,
                Title = "SoundFlow",
                Description = "Une expérience musicale immersive avec une architecture moderne et évolutive",
                
                Technologies = new[]
                {
                     ("C#", "Langage puissant et flexible", Icons.Material.Filled.Code),
                    (".NET 8", "Framework moderne et performant", Icons.Custom.Brands.Microsoft),
                    ("Blazor", "UI interactive et réactive", Icons.Custom.Brands.Microsoft),
                    ("Clean Architecture", "Architecture modulaire et maintenable", Icons.Material.Filled.Architecture),
                    ("CQRS", "Séparation des responsabilités", Icons.Material.Filled.CallSplit),
                    ("DDD", "Conception dirigée par le domaine", Icons.Material.Filled.Domain),
                    ("MudBlazor", "Composants UI modernes", Icons.Material.Filled.Palette),
                    ("Entity Framework", "ORM puissant et flexible", Icons.Material.Filled.Storage),
                    ("SignalR", "Communication en temps réel", Icons.Material.Filled.Stream)
                },
                    Features = new[]
                {
                    ("Streaming HD", "Profitez de votre musique en haute qualité avec un streaming fluide et sans interruption", Icons.Material.Filled.HighQuality),
                    ("Playlists Collaboratives", "Créez et partagez des playlists avec vos amis", Icons.Material.Filled.Group),
                    ("Mode Hors-ligne", "Téléchargez vos morceaux favoris pour les écouter sans connexion", Icons.Material.Filled.CloudDownload),
                    ("Recommandations", "Découvrez de nouveaux artistes grâce à notre système de recommandation", Icons.Material.Filled.Recommend),
                    ("Multi-plateformes", "Accédez à votre musique sur tous vos appareils", Icons.Material.Filled.Devices),
                    ("Clean Architecture", "Une architecture moderne et évolutive pour une expérience optimale", Icons.Material.Filled.Architecture)
                },
                Images = new[]
                {
                    "/Images/Projects/SoundFlow/accueil.png",
                    "/Images/Projects/SoundFlow/accueil2.png",
                    "/Images/Projects/SoundFlow/addcollaborator.png",
                    "/Images/Projects/SoundFlow/addsong.png",
                    "/Images/Projects/SoundFlow/artistdetails.png",
                    "/Images/Projects/SoundFlow/artists.png",
                    "/Images/Projects/SoundFlow/compte1.png",
                    "/Images/Projects/SoundFlow/device.png",
                    "/Images/Projects/SoundFlow/favorite.png",
                    "/Images/Projects/SoundFlow/notifications.png",
                    "/Images/Projects/SoundFlow/playlist.png",
                    "/Images/Projects/SoundFlow/playlistdetails.png",
                    "/Images/Projects/SoundFlow/playlistdetails2.png",
                    "/Images/Projects/SoundFlow/privateprofile.png",
                    "/Images/Projects/SoundFlow/queue.png",
                    "/Images/Projects/SoundFlow/securite.png",
                    "/Images/Projects/SoundFlow/songs.png",
                    "/Images/Projects/SoundFlow/userartists.png",
                    "/Images/Projects/SoundFlow/userplaylists.png",
                    "/Images/Projects/SoundFlow/users.png",
                },
                GitHubUrl = "https://github.com/votre-username/music-streaming",
                ImageUrl = "/Images/Projects/SoundFlow/queue.png"
            },

            new Project
            {
                Id = 2,
                Title = "FleetFactory",
                Description = "Une application pour gérer un parc automobile avec des fonctionnalités de création, recherche et gestion des véhicules.",
                Technologies = new[]
                {
                     ("C#", "Langage puissant et flexible", Icons.Material.Filled.Code),
                    (".NET 8", "Framework robuste et moderne", Icons.Custom.Brands.Microsoft),
                    ("Factory Pattern", "Création simplifiée des objets", Icons.Material.Filled.Factory)
                },
                Features = new[]
                {
                    ("Gestion des véhicules", "Ajoutez, modifiez et supprimez des véhicules dans le parc", Icons.Material.Filled.CarRental),
                    ("Recherche intuitive", "Recherchez des véhicules par immatriculation ou par type", Icons.Material.Filled.Search),
                    ("Visualisation", "Affichez la liste complète des véhicules avec leurs détails", Icons.Material.Filled.List)
                },
                Images = new[]
                {
                    "/Images/Projects/FleetFactory/fleetFactory.png"
            
                },
                GitHubUrl = "https://github.com/votre-username/gestion-parc-automobile",
                ImageUrl = "/Images/Projects/FleetFactory/fleetFactory.png"
            },
            new Project
            {
                Id = 3,
                Title = "DeliveryPlanning",
                Description = "Une application qui permet de planifier les itinéraires de livraison en fonction de différentes stratégies (distance la plus courte, coût le plus bas, temps le plus rapide).",
                Technologies = new[]
                {
                    (".NET 8", "Framework robuste et moderne", Icons.Custom.Brands.Microsoft),
                    ("C#", "Langage puissant et flexible", Icons.Material.Filled.Code),
                    ("Strategy Pattern", "Gestion des itinéraires avec des stratégies dynamiques", Icons.Material.Filled.Timeline)
                },
                Features = new[]
                {
                    ("Ajout de Livraisons", "Ajoutez des livraisons avec des adresses de départ et d’arrivée", Icons.Material.Filled.AddLocationAlt),
                    ("Choix de Stratégie", "Sélectionnez une stratégie d’itinéraire comme distance la plus courte, coût le plus bas ou temps le plus rapide", Icons.Material.Filled.FilterList),
                    ("Génération d’Itinéraire", "Générez l’itinéraire optimal en fonction de la stratégie choisie", Icons.Material.Filled.Route)
                },
                Images = new[]
                {
                    "/Images/Projects/DeliveryPlanning/deliveryPlanning.png"
                },
                GitHubUrl = "https://github.com/votre-username/delivery-planner",
                ImageUrl = "/Images/Projects/DeliveryPlanning/deliveryPlanning.png"
            },
            new Project
            {
                Id = 4,
                Title = "BankApp",
                Description = "Un système bancaire minimaliste avec gestion des comptes, des transactions, et des intérêts basé sur une architecture modulaire.",

                Technologies = new[]
                {
                    ("C#", "Langage puissant et flexible", Icons.Material.Filled.Code),
                    (".NET 8", "Framework moderne et performant", Icons.Custom.Brands.Microsoft),
                    ("Clean Architecture", "Architecture modulaire et maintenable", Icons.Material.Filled.Architecture),
                    ("Singleton Pattern", "Gestion d'une instance unique", Icons.Material.Filled.Pattern),
                },
                Features = new[]
                {
                    ("Création de Comptes", "Ajoutez de nouveaux comptes utilisateurs avec un solde initial", Icons.Material.Filled.PersonAdd),
                    ("Dépôts et Retraits", "Effectuez des opérations bancaires courantes", Icons.Material.Filled.AttachMoney),
                    ("Transferts d'Argent", "Transférez de l'argent entre comptes de manière sécurisée", Icons.Material.Filled.SwapHoriz),
                    ("Historique des Transactions", "Consultez toutes les transactions associées à un compte", Icons.Material.Filled.History),
                    ("Calcul des Intérêts", "Générez des intérêts automatiquement pour les comptes épargne", Icons.Material.Filled.Calculate),
                },
                Images = new[]
                {
                    "/Images/Projects/BankApp/BankApp.png"
                    
                },
                GitHubUrl = "https://github.com/votre-username/banking-system",
                ImageUrl =  "/Images/Projects/BankApp/BankApp.png"
            },
            new Project
            {
                Id = 5,
                Title = "CV Generator",
                Description = "Une application permettant aux utilisateurs de créer, prévisualiser et télécharger des CV professionnels en PDF, avec un design moderne et des options de personnalisation avancées.",
                Technologies = new[]
                {
                    ("C#", "Un langage puissant et flexible", Icons.Material.Filled.Code),
                    (".NET 8", "Un framework robuste et moderne", Icons.Custom.Brands.Microsoft),
                    ("Blazor", "Interface web interactive avec C#", Icons.Material.Filled.Web),
                    ("MudBlazor", "Framework basé sur Material Design pour Blazor", Icons.Material.Filled.Palette),
                    ("HTML2PDF", "Bibliothèque pour la génération de PDF", Icons.Material.Filled.PictureAsPdf)
                },
                Features = new[]
                {
                    ("Design Moderne", "Un design en deux colonnes optimisé pour tenir sur une page", Icons.Material.Filled.DesignServices),
                    ("Sections Personnalisables", "Ajoutez des informations personnelles, expériences professionnelles, formations, compétences, langues et centres d'intérêts", Icons.Material.Filled.Edit),
                    ("Prévisualisation en Temps Réel", "Visualisez les mises à jour de votre CV avant de le télécharger", Icons.Material.Filled.Preview),
                    ("Exportation en PDF", "Téléchargez votre CV complet sous un format PDF soigné", Icons.Material.Filled.Download),
                    ("Interface Responsive", "Compatible avec tous les appareils", Icons.Material.Filled.Devices)
                },
                Images = new[]
                {
                    "/Images/Projects/CvGenerator/cv-generator.png"
                },
                GitHubUrl = "https://github.com/votre-username/cv-generator",
                ImageUrl = "/Images/Projects/CvGenerator/cv-generator.png"
            },


        };

        public static Project? Get(int id)
        {
            return  _projects.FirstOrDefault(p => p.Id == id);
        }
        public static List<Project> GetAll()
        {
          return  _projects.Count > 0 ? _projects : [];
        }
    }
}
