using System.ComponentModel.DataAnnotations;
public class Measurement {
    [Key]
    public int Id { get; set; }
    public string? ApparatusId { get; set; }
    public string? ApparatusVersion {get;set;}
    public string? ApparatusSensorType {get;set;}
    public string? ApparatusTubeType {get;set;}
    public string? Temperature {get;set;}
    public string? Value {get;set;}
    public string? HitsNumer {get;set;}
    public string? CalibrationFunction {get;set;}
    public string? StartTime {get;set;}
    public string? EndTime{get;set;}
}