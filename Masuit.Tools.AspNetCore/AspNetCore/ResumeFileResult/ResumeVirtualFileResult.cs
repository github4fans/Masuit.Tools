﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Net.Http.Headers;

namespace Masuit.Tools.AspNetCore.ResumeFileResults.ResumeFileResult;

/// <summary>
/// 基于服务器虚拟路径路径的ResumePhysicalFileResult
/// </summary>
public class ResumeVirtualFileResult : VirtualFileResult, IResumeFileResult
{
    /// <summary>
    /// 基于服务器虚拟路径路径的ResumePhysicalFileResult
    /// </summary>
    /// <param name="fileName">文件全路径</param>
    /// <param name="contentType">Content-Type</param>
    /// <param name="etag">ETag</param>
    public ResumeVirtualFileResult(string fileName, string contentType, string etag = null) : this(fileName, MediaTypeHeaderValue.Parse(contentType), !string.IsNullOrEmpty(etag) ? EntityTagHeaderValue.Parse(etag) : null)
    {
    }

    /// <summary>
    /// 基于服务器虚拟路径路径的ResumePhysicalFileResult
    /// </summary>
    /// <param name="fileName">文件全路径</param>
    /// <param name="contentType">Content-Type</param>
    /// <param name="etag">ETag</param>
    public ResumeVirtualFileResult(string fileName, MediaTypeHeaderValue contentType, EntityTagHeaderValue etag = null) : base(fileName, contentType)
    {
        EntityTag = etag;
        EnableRangeProcessing = true;
    }

    /// <inheritdoc/>
    public string FileInlineName { get; set; }

    /// <inheritdoc/>
    public override Task ExecuteResultAsync(ActionContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        var executor = context.HttpContext.RequestServices.GetRequiredService<IActionResultExecutor<ResumeVirtualFileResult>>();
        return executor.ExecuteAsync(context, this);
    }
}
