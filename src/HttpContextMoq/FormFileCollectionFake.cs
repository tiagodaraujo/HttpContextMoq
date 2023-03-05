using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace HttpContextMoq;

public class FormFileCollectionFake : List<IFormFile>, IFormFileCollection
{
    public IFormFile this[string name] => GetFile(name);

    public IFormFile GetFile(string name)
    {
        foreach (var file in this)
        {
            if (string.Equals(name, file.Name, StringComparison.OrdinalIgnoreCase))
            {
                return file;
            }
        }

        return null;
    }

    public IReadOnlyList<IFormFile> GetFiles(string name)
    {
        var files = new List<IFormFile>();

        foreach (var file in this)
        {
            if (string.Equals(name, file.Name, StringComparison.OrdinalIgnoreCase))
            {
                files.Add(file);
            }
        }

        return files;
    }
}
