﻿using System;
using System.IO;
using System.Threading.Tasks;

namespace Tiny.RestClient
{
    /// <summary>
    /// Interface IRequest
    /// </summary>
    /// <seealso cref="IFormRequest" />
    /// <seealso cref="IExecutableRequest" />
    public interface IRequest : IExecutableRequest, IFormRequest
    {
        /// <summary>
        /// Add a basic authentication credentials
        /// </summary>
        /// <param name="username">the username</param>
        /// <param name="password">the password</param>
        /// <returns>The current request</returns>
        IRequest WithBasicAuthentication(string username, string password);

        /// <summary>
        /// Add a basic authentication credentials
        /// The delegate will be executed right before ExecuteAsync()
        /// </summary>
        /// <param name="basicAuthenticationFunc">a delegate</param>
        /// <returns>The current request</returns>
        IRequest WithBasicAuthentication(Func<Task<Tuple<string, string>>> basicAuthenticationFunc);

        /// <summary>
        /// Add a bearer token in the request headers
        /// </summary>
        /// <param name="token">token value</param>
        /// <returns>The current request</returns>
        IRequest WithOAuthBearer(string token);

        /// <summary>
        /// Add a bearer token in the request headers
        /// The delegate will be executed right before ExecuteAsync()
        /// </summary>
        /// <param name="oAuthFunc">a delegate</param>
        /// <returns>The current request</returns>
        IRequest WithOAuthBearer(Func<Task<string>> oAuthFunc);

        /// <summary>
        /// With timeout for current request
        /// </summary>
        /// <param name="timeout">timeout</param>
        /// <returns>The current request</returns>
        IRequest WithTimeout(TimeSpan timeout);

        /// <summary>
        /// Adds the content.
        /// </summary>
        /// <typeparam name="TContent">The type of the t content.</typeparam>
        /// <param name="content">The content.</param>
        /// <param name="serializer">Override the default serializer setted on the client.</param>
        /// <param name="compression">Add a compression system to compress your content</param>
        /// <returns>The current request</returns>
        IParameterRequest AddContent<TContent>(TContent content, IFormatter serializer = null, ICompression compression = null);

        /// <summary>
        /// Add byte array as content of request
        /// </summary>
        /// <param name="byteArray">The byte array.</param>
        /// <param name="contentType">The Content type</param>
        /// <returns>The current request</returns>
        IParameterRequest AddByteArrayContent(byte[] byteArray, string contentType = "application/octet-stream");

        /// <summary>
        /// Adds stream as content
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="contentType">The content type</param>
        /// <returns>The current request</returns>
        IParameterRequest AddStreamContent(Stream stream, string contentType = "application/octet-stream");
#if !FILEINFO_NOT_SUPPORTED
        /// <summary>
        /// Adds file as content.
        /// </summary>
        /// <param name="file">The file to add as content of request.</param>
        /// <param name="contentType">The content type</param>
        /// <returns>The current request</returns>
        IParameterRequest AddFileContent(FileInfo file, string contentType);
#endif

        /// <summary>
        /// As a multipart data from request
        /// </summary>
        /// <param name="contentType">content type of the request (default value  = "multipart/form-data")</param>
        /// <returns>The current request</returns>
        IMultipartFromDataRequest AsMultiPartFromDataRequest(string contentType = "multipart/form-data");
    }
}