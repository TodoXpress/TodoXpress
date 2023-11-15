namespace TodoXpress.Api.Data.DependencyInjection;

public static class PipelineConfiguration
{
    /// <summary>
    /// Adds static swagger files.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> to which swagger will be added.</param>
    /// <returns>The <see cref="WebApplication"/>.</returns>
    public static WebApplication UseSwaggerInDev(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        return app;
    }
}
