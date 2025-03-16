var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithDataVolume()
    .WithPgAdmin(options => options.WithHostPort(5432));

var db = postgres.AddDatabase("fstimedb");

var migrationService = builder.AddProject<Projects.FSTime_Services_DatabaseMigration>("MigrationService")
    .WithReference(db)
    .WaitFor(db);

var api = builder.AddProject<Projects.FSTime_Api>("Api")
    .WithReference(db)
    .WaitFor(db);

var client = builder.AddNpmApp("Client", "../Client", "dev")
    .WithNpmPackageInstallation()
    .WithReference(api)
    .WaitFor(api)
    .WithEnvironment("ApiUrl", api.GetEndpoint("https"))
    .WithEnvironment("BROWSER", "none") // Disable opening browser on npm start
    .WithHttpEndpoint(env: "PORT", port: 3000)
    .WithExternalHttpEndpoints()
    .PublishAsDockerFile();
;

builder.Build().Run();
