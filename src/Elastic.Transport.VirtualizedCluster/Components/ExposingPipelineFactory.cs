// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

namespace Elastic.Transport.VirtualizedCluster.Components
{
	/// <summary>
	/// An implementation that exposes all the components so that <see cref="VirtualCluster"/> can reference them directly.
	/// </summary>
	public class ExposingPipelineFactory : IRequestPipelineFactory
	{
		public ExposingPipelineFactory(ITransportConfigurationValues connectionSettings, IDateTimeProvider dateTimeProvider)
		{
			DateTimeProvider = dateTimeProvider;
			MemoryStreamFactory = TransportConfiguration.DefaultMemoryStreamFactory;

			Settings = connectionSettings;
			Pipeline = Create(Settings, DateTimeProvider, MemoryStreamFactory, new RequestParameters(HttpMethod.GET, supportsBody: false));
			Transport = new Transport<ITransportConfigurationValues>(Settings, this, DateTimeProvider, MemoryStreamFactory);
		}

		// ReSharper disable once MemberCanBePrivate.Global
		public IRequestPipeline Pipeline { get; }

		private IDateTimeProvider DateTimeProvider { get; }
		private IMemoryStreamFactory MemoryStreamFactory { get; }
		private ITransportConfigurationValues Settings { get; }
		public ITransport<ITransportConfigurationValues> Transport { get; }


		public IRequestPipeline Create(ITransportConfigurationValues configurationValues, IDateTimeProvider dateTimeProvider,
			IMemoryStreamFactory memoryStreamFactory, IRequestParameters requestParameters
		) =>
			new RequestPipeline(Settings, DateTimeProvider, MemoryStreamFactory, requestParameters ?? new RequestParameters(HttpMethod.GET, supportsBody: false));
	}
}
