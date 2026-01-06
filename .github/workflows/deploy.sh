#!/bin/bash

# Script de déploiement manuel pour VPS Ubuntu
# Usage: ./deploy.sh

set -e

echo "🚀 Déploiement de MyPortfolio sur le VPS..."

# Variables (à adapter selon votre configuration)
VPS_HOST="${VPS_HOST:-your-vps-ip}"
VPS_USER="${VPS_USER:-root}"
VPS_PORT="${VPS_PORT:-22}"
DEPLOY_PATH="/var/www/myportfolio"

# Couleurs pour les messages
GREEN='\033[0;32m'
RED='\033[0;31m'
NC='\033[0m' # No Color

# Étape 1: Build du projet
echo -e "${GREEN}📦 Construction du projet...${NC}"
dotnet publish --configuration Release --output ./publish

# Étape 2: Copie des fichiers sur le VPS
echo -e "${GREEN}📤 Envoi des fichiers sur le VPS...${NC}"
scp -P $VPS_PORT -r ./publish/wwwroot/* $VPS_USER@$VPS_HOST:$DEPLOY_PATH/

# Étape 3: Redémarrage de Nginx
echo -e "${GREEN}🔄 Redémarrage de Nginx...${NC}"
ssh -p $VPS_PORT $VPS_USER@$VPS_HOST "sudo systemctl reload nginx"

echo -e "${GREEN}✅ Déploiement terminé avec succès!${NC}"

