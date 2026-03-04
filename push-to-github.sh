#!/bin/bash
cd /c/Users/JUAND/proyecto-pedidos
git init
git config user.name "BL4Z3YT"
git config user.email "juandavid4353@gmail.com"
git add .
echo "=== Git Status ==="
git status
echo ""
echo "=== Creating commit ==="
git commit -m "init: README con descripción del problema y diseño IPO"
echo ""
echo "=== Setting up remote ==="
git branch -M main
git remote add origin https://github.com/BL4Z3YT/proyecto-pedidos.git
echo ""
echo "=== Pushing to GitHub ==="
git push -u origin main
echo ""
echo "=== Done! ==="
