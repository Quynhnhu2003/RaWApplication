remove-migration

add-migration -context RaWDbContext ten-migration
update-database

Add-Migration CreateIdentitySchema -context RaWIdentityContext
update-database -context RaWIdentityContext 