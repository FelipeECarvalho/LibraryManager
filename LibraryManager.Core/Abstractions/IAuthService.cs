﻿namespace LibraryManager.Core.Abstractions
{
    using LibraryManager.Core.Entities;

    public interface ITokenProvider
    {
        Task<string> GenerateTokenAsync(User user);
    }
}
