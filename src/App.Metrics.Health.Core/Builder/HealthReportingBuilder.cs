﻿// <copyright file="HealthReportingBuilder.cs" company="App Metrics Contributors">
// Copyright (c) App Metrics Contributors. All rights reserved.
// </copyright>

using System;

namespace App.Metrics.Health.Builder
{
    public class HealthReportingBuilder : IHealthReportingBuilder
    {
        private readonly Action<IReportHealthStatus> _reporters;

        public HealthReportingBuilder(
            IHealthBuilder healthBuilder,
            Action<IReportHealthStatus> reporters)
        {
            Builder = healthBuilder ?? throw new ArgumentNullException(nameof(healthBuilder));
            _reporters = reporters ?? throw new ArgumentNullException(nameof(reporters));
        }

        /// <inheritdoc />
        public IHealthBuilder Builder { get; }

        /// <inheritdoc />
        public IHealthBuilder Using(IReportHealthStatus reporter)
        {
            if (reporter == null)
            {
                throw new ArgumentNullException(nameof(reporter));
            }

            _reporters(reporter);

            return Builder;
        }

        /// <inheritdoc />
        public IHealthBuilder Using<TReportHealth>()
            where TReportHealth : IReportHealthStatus, new()
        {
            var reporter = new TReportHealth();

            return Using(reporter);
        }
    }
}