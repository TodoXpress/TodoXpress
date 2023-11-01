namespace TodoXpress.Domain.Calendars.ValueTypes;

public struct Location(double lon, double lat)
{
    public double Longitute { get; set; } = lon; 
    
    public double Latitute { get; set; } = lat;
}
