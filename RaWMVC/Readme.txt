remove-migration

add-migration AddRaWDbContext -OutputDir Data/Migrations
update-database

Add-Migration CreateIdentitySchema -context RaWIdentityContext -OutputDir Data/IdentityMigrations
update-database -context RaWIdentityContext 