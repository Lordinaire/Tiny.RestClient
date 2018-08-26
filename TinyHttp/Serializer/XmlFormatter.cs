﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace Tiny.Http
{
    /// <summary>
    /// Class TinyXmlSerializer.
    /// </summary>
    /// <seealso cref="Tiny.Http.IFormatter" />
    public class XmlFormatter : IFormatter
    {
        /// <inheritdoc/>
        public string DefaultMediaType => "application/xml";

        /// <inheritdoc/>
        public IEnumerable<string> SupportedMediaTypes
        {
            get
            {
                yield return "application/xml";
                yield return "text/xml";
            }
        }

        /// <inheritdoc/>
        public T Deserialize<T>(Stream stream)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
            catch (Exception ex)
            {
                string data = null;
                stream.Position = 0;
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    data = reader.ReadToEnd();
                }

                throw new DeserializeException("Error during deserialization", ex, data);
            }
        }

        /// <inheritdoc/>
        public string Serialize<T>(T data, Encoding encoding)
        {
            if (data == default)
            {
                return null;
            }

            var serializer = new XmlSerializer(data.GetType());
            using (var stringWriter = new DynamicEncodingStringWriter(encoding))
            {
                serializer.Serialize(stringWriter, data);
                return stringWriter.ToString();
            }
        }
    }
}
