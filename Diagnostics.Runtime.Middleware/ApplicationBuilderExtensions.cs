﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Diagnostics.Runtime.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseClrMd(this IApplicationBuilder builder)
        {
            string basePath = "/diagnostics";

            builder.Map(new PathString($"{basePath}/stacks"), x => x.UseMiddleware<StacksDiagnosticsMiddleware>());
            builder.Map(new PathString($"{basePath}/runtime"), x => x.UseMiddleware<RuntimeDiagnosticsMiddleware>());
            builder.Map(new PathString($"{basePath}/heap"), x => x.UseMiddleware<HeapDiagnosticsMiddleware>());
            builder.Map(new PathString($"{basePath}/threads"), x => x.UseMiddleware<ThreadsDiagnosticsMiddleware>());

            builder.Map(new PathString(basePath), x =>
                x.UseMiddleware<RuntimeDiagnosticsMiddleware>()
                 .UseMiddleware<StacksDiagnosticsMiddleware>()
                 .UseMiddleware<HeapDiagnosticsMiddleware>()
                 .UseMiddleware<ThreadsDiagnosticsMiddleware>());

            return builder;
        }
    }
}
