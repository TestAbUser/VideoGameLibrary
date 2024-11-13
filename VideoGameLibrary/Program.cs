using VideoGameLibrary.EndpointFilters;
using VideoGameLibrary.ExceptionHandlers;
using VideoGameLibrary.Mapping;
using VideoGameLibrary.Mapping.Interfaces;
using VideoGameLibrary.Models;
using BLL.Services;
using Domain.DTOs;
using Domain.Repositories;
using Domain.Services;
using FluentValidation;
using Infrastructure.SQL.Database;
using Infrastructure.SQL.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var dbConnection = builder.Configuration.GetConnectionString("LibraryDb");
builder.Services.AddDbContextPool<LibraryContext>(options =>
    options.UseSqlServer(dbConnection, sqlServerOptionsAction: sqlOptions =>
    {
        sqlOptions.EnableRetryOnFailure(maxRetryCount: 3);
    }));

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddScoped<IVideoGameMapper, VideoGameMapper>();
builder.Services.AddScoped<IVideoGameService, VideoGameService>();
builder.Services.AddScoped<IVideoGameRepository, VideoGameRepository>();
builder.Services.AddExceptionHandler<TimeOutExceptionHandler>();
builder.Services.AddExceptionHandler<DefaultExceptionHandler>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/videoGames", async ([FromBody] VideoGame game, IVideoGameMapper mapper, IVideoGameService gameService) =>
{
    var gameDto = mapper.Map(game);
    var gameId = await gameService.CreateAsync(gameDto);
    if (gameId <= 0)
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }

    return Results.CreatedAtRoute("videoGameById", new { Id = gameId });
}).AddEndpointFilter<InputValidatorFilter<VideoGame>>();

app.MapGet("/videoGames/{id}", async (IVideoGameMapper mapper, int id, IVideoGameService gameService) =>
{
    var game = await gameService.RetrieveAsync(id);

    if (game is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(mapper.Map(game));
}).WithName("videoGameById");

app.MapGet("/videoGames/filteredVideoGames/{genres}", async (IVideoGameMapper mapper, string[] genres, IVideoGameService gameService) =>
{
    var game = await gameService.RetrieveFilteredAsync(genres);

    if (game is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(mapper.Map(game));
});

app.MapGet("/videoGames", async (IVideoGameMapper mapper, IVideoGameService gameService) =>
{
    var games = await gameService.GetAllAsync();
    return Results.Ok(mapper.Map(games));
});

app.MapDelete("/videoGames/{id}", async (int id, IVideoGameService gameService) =>
{
    if (await gameService.DeleteAsync(id))
    {
        return Results.NoContent();
    }
    return Results.NotFound();
});

app.MapPut("/videoGames", async ([FromBody] VideoGame game, IVideoGameMapper mapper, IVideoGameService gameService) =>
{
    var gameDto = mapper.Map(game);
    var gameId = await gameService.UpdateAsync(gameDto);
    if (gameId <= 0)
    {
        return Results.StatusCode(StatusCodes.Status500InternalServerError);
    }
    if (game.Id is null)
    {
        return Results.CreatedAtRoute("videoGameById", new { Id = gameId });
    }
    return Results.NoContent();
}).AddEndpointFilter<InputValidatorFilter<VideoGame>>();

app.MapPatch("/games/{id}", async ([FromBody] VideoGamePatch game, int id, IVideoGameMapper mapper, IVideoGameService gameService) =>
{
    if (await gameService.UpdateGenresAsync(id, game.Genres))
    {
        return Results.NoContent();
    }
    return Results.NotFound();
}).AddEndpointFilter<InputValidatorFilter<VideoGamePatch>>();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<LibraryContext>();
    db.Database.SetConnectionString(dbConnection);
    db.Database.Migrate();
}

app.Run();