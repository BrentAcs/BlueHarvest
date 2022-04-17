using MongoDB.Driver;

namespace BlueHarvest.Core.Utilities;

public record MinMax<T>(T? Min=default, T? Max=default);
