﻿namespace DataManaging.Contract
{
    public interface IDataSaver<T>
    {
        void SaveObject(T data);
        void SetName(string name);
    }
}