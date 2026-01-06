#!/bin/bash

# Script de configuration manuelle du VPS
# À exécuter directement sur le VPS

set -e

echo "🚀 Configuration du VPS pour MyPortfolio..."

# Création du répertoire de déploiement
echo "📁 Création du répertoire de déploiement..."
sudo mkdir -p /var/www/myportfolio
sudo chown -R $USER:$USER /var/www/myportfolio
sudo chmod -R 755 /var/www/myportfolio

# Création d'un fichier index.html temporaire
echo "📝 Création d'un fichier de test..."
cat > /var/www/myportfolio/index.html << 'EOF'
<!DOCTYPE html>
<html>
<head>
    <title>MyPortfolio - En attente de déploiement</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
        }
        .container {
            text-align: center;
            padding: 2rem;
            background: rgba(255, 255, 255, 0.1);
            border-radius: 10px;
            backdrop-filter: blur(10px);
        }
        h1 { margin-top: 0; }
    </style>
</head>
<body>
    <div class="container">
        <h1>🚀 MyPortfolio</h1>
        <p>Le répertoire est prêt pour le déploiement !</p>
        <p>Une fois le workflow GitHub Actions terminé, votre site sera disponible ici.</p>
    </div>
</body>
</html>
EOF

echo "✅ Répertoire créé avec succès!"
echo "📍 Chemin: /var/www/myportfolio"
echo ""
echo "📋 Prochaines étapes:"
echo "1. Configurez Nginx (voir nginx/myportfolio.conf)"
echo "2. Lancez le workflow GitHub Actions pour déployer votre application"

