namespace TodoXpress.Domain.Common.ValueTypes;

/// <summary>
/// Represents the Geolocation.
/// </summary>
/// <param name="Lon">The Longitute.</param>
/// <param name="Lat">The Latitute.</param>
public record struct Location(double Long, double Lat);
