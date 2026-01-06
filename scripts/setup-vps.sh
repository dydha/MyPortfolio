#!/bin/bash

# Script d'installation et de configuration initiale du VPS
# Usage: ./setup-vps.sh
# Exécutez ce script directement sur votre VPS Ubuntu

set -e

echo "🚀 Configuration du VPS pour MyPortfolio..."

# Couleurs
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m'

# Mise à jour du système
echo -e "${GREEN}📦 Mise à jour du système...${NC}"
sudo apt update && sudo apt upgrade -y

# Installation de Nginx
echo -e "${GREEN}🌐 Installation de Nginx...${NC}"
sudo apt install nginx -y
sudo systemctl enable nginx
sudo systemctl start nginx

# Création du répertoire de déploiement
echo -e "${GREEN}📁 Création du répertoire de déploiement...${NC}"
sudo mkdir -p /var/www/myportfolio
sudo chown -R $USER:$USER /var/www/myportfolio
sudo chmod -R 755 /var/www/myportfolio

# Création d'un fichier index.html temporaire
echo -e "${GREEN}📝 Création d'un fichier de test...${NC}"
cat > /var/www/myportfolio/index.html << EOF
<!DOCTYPE html>
<html>
<head>
    <title>MyPortfolio - En attente de déploiement</title>
</head>
<body>
    <h1>MyPortfolio</h1>
    <p>Le déploiement est en cours de configuration...</p>
</body>
</html>
EOF

# Installation de Certbot pour SSL (optionnel)
echo -e "${YELLOW}🔒 Voulez-vous installer Certbot pour SSL ? (y/n)${NC}"
read -r response
if [[ "$response" =~ ^([yY][eE][sS]|[yY])$ ]]; then
    echo -e "${GREEN}🔐 Installation de Certbot...${NC}"
    sudo apt install certbot python3-certbot-nginx -y
    echo -e "${GREEN}✅ Certbot installé. Utilisez 'sudo certbot --nginx -d votre-domaine.com' pour configurer SSL${NC}"
fi

echo -e "${GREEN}✅ Configuration du VPS terminée!${NC}"
echo -e "${YELLOW}📋 Prochaines étapes:${NC}"
echo "1. Configurez votre domaine dans /etc/nginx/sites-available/myportfolio"
echo "2. Créez un lien symbolique: sudo ln -s /etc/nginx/sites-available/myportfolio /etc/nginx/sites-enabled/"
echo "3. Testez la configuration: sudo nginx -t"
echo "4. Rechargez Nginx: sudo systemctl reload nginx"
echo "5. Configurez les secrets GitHub Actions pour le déploiement automatique"

