/* Copyright 2013-present MongoDB Inc.
*
* Licensed under the Apache License, Version 2.0 (the "License");
* you may not use this file except in compliance with the License.
* You may obtain a copy of the License at
*
* http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing, software
* distributed under the License is distributed on an "AS IS" BASIS,
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
* See the License for the specific language governing permissions and
* limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver.Core.Bindings;
using MongoDB.Driver.Core.Events;
using MongoDB.Driver.Core.Misc;
using MongoDB.Driver.Core.WireProtocol.Messages.Encoders;

namespace MongoDB.Driver.Core.Operations
{
    /// <summary>
    /// Represents a create indexes operation.
    /// </summary>
    public class CreateIndexesOperation : IWriteOperation<BsonDocument>
    {
        // fields
        private readonly CollectionNamespace _collectionNamespace;
        private CreateIndexCommitQuorum _commitQuorum;
        private BsonValue _comment;
        private TimeSpan? _maxTime;
        private readonly MessageEncoderSettings _messageEncoderSettings;
        private readonly IEnumerable<CreateIndexRequest> _requests;
        private WriteConcern _writeConcern = WriteConcern.Acknowledged;

        // constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateIndexesOperation"/> class.
        /// </summary>
        /// <param name="collectionNamespace">The collection namespace.</param>
        /// <param name="requests">The requests.</param>
        /// <param name="messageEncoderSettings">The message encoder settings.</param>
        public CreateIndexesOperation(
            CollectionNamespace collectionNamespace,
            IEnumerable<CreateIndexRequest> requests,
            MessageEncoderSettings messageEncoderSettings)
        {
            _collectionNamespace = Ensure.IsNotNull(collectionNamespace, nameof(collectionNamespace));
            _requests = Ensure.IsNotNull(requests, nameof(requests)).ToList();
            _messageEncoderSettings = Ensure.IsNotNull(messageEncoderSettings, nameof(messageEncoderSettings));
        }

        // properties
        /// <summary>
        /// Gets the collection namespace.
        /// </summary>
        /// <value>
        /// The collection namespace.
        /// </value>
        public CollectionNamespace CollectionNamespace
        {
            get { return _collectionNamespace; }
        }

        /// <summary>
        /// Gets or sets the comment.
        /// </summary>
        public BsonValue Comment
        {
            get { return _comment; }
            set { _comment = value; }
        }

        /// <summary>
        /// Gets or sets the commit quorum.
        /// </summary>
        public CreateIndexCommitQuorum CommitQuorum
        {
            get => _commitQuorum;
            set => _commitQuorum = value;
        }

        /// <summary>
        /// Gets the message encoder settings.
        /// </summary>
        /// <value>
        /// The message encoder settings.
        /// </value>
        public MessageEncoderSettings MessageEncoderSettings
        {
            get { return _messageEncoderSettings; }
        }

        /// <summary>
        /// Gets the create index requests.
        /// </summary>
        /// <value>
        /// The create index requests.
        /// </value>
        public IEnumerable<CreateIndexRequest> Requests
        {
            get { return _requests; }
        }

        /// <summary>
        /// Gets or sets the write concern.
        /// </summary>
        /// <value>
        /// The write concern.
        /// </value>
        public WriteConcern WriteConcern
        {
            get { return _writeConcern; }
            set { _writeConcern = Ensure.IsNotNull(value, nameof(value)); }
        }

        /// <summary>
        /// Gets or sets the max time.
        /// </summary>
        /// <value>
        /// The max time
        /// </value>
        public TimeSpan? MaxTime
        {
            get { return _maxTime; }
            set { _maxTime = Ensure.IsNullOrInfiniteOrGreaterThanOrEqualToZero(value, nameof(value)); }
        }

        // public methods
        /// <inheritdoc/>
        public BsonDocument Execute(IWriteBinding binding, CancellationToken cancellationToken)
        {
            using (EventContext.BeginOperation())
            using (var channelSource = binding.GetWriteChannelSource(cancellationToken))
            using (var channel = channelSource.GetChannel(cancellationToken))
            using (var channelBinding = new ChannelReadWriteBinding(channelSource.Server, channel, binding.Session.Fork()))
            {
                var operation = CreateOperation();
                return operation.Execute(channelBinding, cancellationToken);
            }
        }

        /// <inheritdoc/>
        public async Task<BsonDocument> ExecuteAsync(IWriteBinding binding, CancellationToken cancellationToken)
        {
            using (EventContext.BeginOperation())
            using (var channelSource = await binding.GetWriteChannelSourceAsync(cancellationToken).ConfigureAwait(false))
            using (var channel = await channelSource.GetChannelAsync(cancellationToken).ConfigureAwait(false))
            using (var channelBinding = new ChannelReadWriteBinding(channelSource.Server, channel, binding.Session.Fork()))
            {
                var operation = CreateOperation();
                return await operation.ExecuteAsync(channelBinding, cancellationToken).ConfigureAwait(false);
            }
        }

        // internal methods
        internal IWriteOperation<BsonDocument> CreateOperation()
        {
            return new CreateIndexesUsingCommandOperation(_collectionNamespace, _requests, _messageEncoderSettings)
            {
                Comment = _comment,
                CommitQuorum = _commitQuorum,
                MaxTime = _maxTime,
                WriteConcern = _writeConcern
            };
        }
    }
}
