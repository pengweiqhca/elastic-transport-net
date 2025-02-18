// Licensed to Elasticsearch B.V under one or more agreements.
// Elasticsearch B.V licenses this file to you under the Apache 2.0 License.
// See the LICENSE file in the project root for more information

using System.Text.Json.Serialization;

namespace Elastic.Transport;

/// <summary>
/// A response from an Elastic product including details about the request/response life cycle. Base class for the built in low level response
/// types, <see cref="StringResponse"/>, <see cref="BytesResponse"/>, <see cref="DynamicResponse"/> and <see cref="VoidResponse"/>
/// </summary>
public abstract class TransportResponse<T> : TransportResponse
{
	/// <summary>
	/// The deserialized body returned by the product.
	/// </summary>
	public T Body { get; protected internal set; }
}

/// <summary>
/// A response as returned by <see cref="HttpTransport{TConnectionSettings}"/> including details about the request/response life cycle.
/// </summary>
public abstract class TransportResponse
{
	/// <summary> Returns details about the API call that created this response. </summary>
	[JsonIgnore]
	// TODO: ApiCallDetails is always set, but nothing enforces it
	// since we use new() generic constraint we can not enforce a protected constructor.
	// ReSharper disable once NotNullOrRequiredMemberIsNotInitialized
	public ApiCallDetails ApiCallDetails { get; internal set; }

	/// <inheritdoc cref="object.ToString"/>
	public override string ToString() => ApiCallDetails?.DebugInformation
		// ReSharper disable once ConstantNullCoalescingCondition
		?? $"{nameof(ApiCallDetails)} not set, likely a bug, reverting to default ToString(): {base.ToString()}";
}

