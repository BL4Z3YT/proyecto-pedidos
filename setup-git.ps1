# Script para inicializar repositorio git y hacer commits
# Ejecutar en PowerShell (como Administrador)

$projectPath = "C:\Users\JUAND\proyecto-pedidos"
Set-Location $projectPath

Write-Host "Inicializando repositorio Git..." -ForegroundColor Green

# Inicializar git
git init

# Configurar usuario (REEMPLAZA CON TUS DATOS)
git config user.name "Tu Nombre Aquí"
git config user.email "tu.email@gmail.com"

Write-Host "Configurando .gitignore y añadiendo archivos..." -ForegroundColor Green

# Añadir todos los archivos
git add .

# Primer commit
git commit -m "init: README con descripción del problema y diseño IPO"

Write-Host "`nPrimer commit listo. Ahora necesitas:" -ForegroundColor Yellow
Write-Host "1. Ir a https://github.com/new" -ForegroundColor Cyan
Write-Host "2. Crear repositorio llamado: proyecto-pedidos" -ForegroundColor Cyan
Write-Host "3. Marcar como PUBLIC y no inicializar con README" -ForegroundColor Cyan
Write-Host "4. Copiar los comandos que GitHub te muestra y ejecutarlos:` -ForegroundColor Cyan
Write-Host "`ngit branch -M main" -ForegroundColor White
Write-Host "git remote add origin https://github.com/TU_USUARIO/proyecto-pedidos.git" -ForegroundColor White
Write-Host "git push -u origin main" -ForegroundColor White

Write-Host "`n¡Listo! El repositorio local está configurado." -ForegroundColor Green
