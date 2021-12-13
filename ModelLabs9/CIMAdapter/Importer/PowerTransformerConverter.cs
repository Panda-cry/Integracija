﻿namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
				if (cimIdentifiedObject.DescriptionHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_DESCRIPTION, cimIdentifiedObject.Description));
				}
			}
		}

		public static void PopulateLocationProperties(FTN.Location cimLocation, ResourceDescription rd)
		{
			if ((cimLocation != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimLocation, rd);

				if (cimLocation.CategoryHasValue)
				{
					rd.AddProperty(new Property(ModelCode.LOCATION_CATEGORY, cimLocation.Category));
				}
				if (cimLocation.CorporateCodeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.LOCATION_CORPORATECODE, cimLocation.CorporateCode));
				}
			}
		}

		public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPowerSystemResource != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);

				if (cimPowerSystemResource.CustomTypeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PSR_CUSTOMTYPE, cimPowerSystemResource.CustomType));
				}
				if (cimPowerSystemResource.LocationHasValue)
				{
					long gid = importHelper.GetMappedGID(cimPowerSystemResource.Location.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimPowerSystemResource.GetType().ToString()).Append(" rdfID = \"").Append(cimPowerSystemResource.ID);
						report.Report.Append("\" - Failed to set reference to Location: rdfID \"").Append(cimPowerSystemResource.Location.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.PSR_LOCATION, gid));
				}
			}
		}

		public static void PopulateBaseVoltageProperties(FTN.BaseVoltage cimBaseVoltage, ResourceDescription rd)
		{
			if ((cimBaseVoltage != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimBaseVoltage, rd);

				if (cimBaseVoltage.NominalVoltageHasValue)
				{
					rd.AddProperty(new Property(ModelCode.BASEVOLTAGE_NOMINALVOLTAGE, cimBaseVoltage.NominalVoltage));
				}
			}
		}

		public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd, importHelper, report);

				if (cimEquipment.PrivateHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_ISPRIVATE, cimEquipment.Private));
				}
				if (cimEquipment.IsUndergroundHasValue)
				{
					rd.AddProperty(new Property(ModelCode.EQUIPMENT_ISUNDERGROUND, cimEquipment.IsUnderground));
				}
			}
		}

		public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimConductingEquipment != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd, importHelper, report);

				if (cimConductingEquipment.PhasesHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CONDEQ_PHASES, (short)GetDMSPhaseCode(cimConductingEquipment.Phases)));
				}
				if (cimConductingEquipment.RatedVoltageHasValue)
				{
					rd.AddProperty(new Property(ModelCode.CONDEQ_RATEDVOLTAGE, cimConductingEquipment.RatedVoltage));
				}
				if (cimConductingEquipment.BaseVoltageHasValue)
				{
					long gid = importHelper.GetMappedGID(cimConductingEquipment.BaseVoltage.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimConductingEquipment.GetType().ToString()).Append(" rdfID = \"").Append(cimConductingEquipment.ID);
						report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimConductingEquipment.BaseVoltage.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.CONDEQ_BASVOLTAGE, gid));
				}
			}
		}

		public static void PopulatePowerTransformerProperties(FTN.PowerTransformer cimPowerTransformer, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPowerTransformer != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateEquipmentProperties(cimPowerTransformer, rd, importHelper, report);

				if (cimPowerTransformer.FunctionHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTR_FUNC, (short)GetDMSTransformerFunctionKind(cimPowerTransformer.Function)));
				}
				if (cimPowerTransformer.AutotransformerHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTR_AUTO, cimPowerTransformer.Autotransformer));
				}
			}
		}
		public static void PopulateTransformerWindingsProperties(FTN.TransformerWinding cimPowerTransformer, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPowerTransformer != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateConductingEquipmentProperties(cimPowerTransformer, rd, importHelper, report);

				//PowerTransformerConverter.PopulatePowerTransformerProperties(cimPowerTransformer, rd, importHelper, report);
				if (cimPowerTransformer.PowerTransformerHasValue)
				{
					long gid = importHelper.GetMappedGID(cimPowerTransformer.PowerTransformer.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimPowerTransformer.GetType().ToString()).Append(" rdfID = \"").Append(cimPowerTransformer.ID);
						report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimPowerTransformer.PowerTransformer.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_POWERTRW, gid));
				}
                if (cimPowerTransformer.ConnectionTypeHasValue)
                {
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_CONNTYPE, (short)GetDMSWindingConnection(cimPowerTransformer.ConnectionType)));
                }
                if (cimPowerTransformer.GroundedHasValue)
                {
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_GROUNDED, cimPowerTransformer.Grounded));
                }
                if (cimPowerTransformer.RatedUHasValue)
                {
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_RATEDU, cimPowerTransformer.RatedU));
				}
				if (cimPowerTransformer.RatedSHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_RATEDS, cimPowerTransformer.RatedS));
				}
				if (cimPowerTransformer.WindingTypeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_WINDTYPE,(short)GetDMSWindingType( cimPowerTransformer.WindingType)));
				}
				if (cimPowerTransformer.PhaseToGroundVoltageHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_PHASETOGRNDVOLTAGE, cimPowerTransformer.PhaseToGroundVoltage));
				}
				if (cimPowerTransformer.PhaseToPhaseVoltageHasValue)
				{
					rd.AddProperty(new Property(ModelCode.POWERTRWINDING_PHASETOPHASEVOLTAGE, cimPowerTransformer.PhaseToPhaseVoltage));
				}
			}
		}

		public static void PopulateWindingTestProperties(FTN.WindingTest cimPowerTransformer, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPowerTransformer != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerTransformer,rd);
				//PowerTransformerConverter.PopulateEquipmentProperties(cimPowerTransformer, rd, importHelper, report);
				if (cimPowerTransformer.From_TransformerWindingHasValue)
				{
					long gid = importHelper.GetMappedGID(cimPowerTransformer.From_TransformerWinding.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(cimPowerTransformer.GetType().ToString()).Append(" rdfID = \"").Append(cimPowerTransformer.ID);
						report.Report.Append("\" - Failed to set reference to BaseVoltage: rdfID \"").Append(cimPowerTransformer.From_TransformerWinding.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.WINDINGTEST_POWERTRWINDING, gid));
				}
                if (cimPowerTransformer.LeakageImpedanceHasValue)
                {
					rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDN, cimPowerTransformer.LeakageImpedance));
                }
				if (cimPowerTransformer.LeakageImpedance0PercentHasValue)
				{
					rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDN0PERCENT, cimPowerTransformer.LeakageImpedance0Percent));
				}
				if (cimPowerTransformer.LeakageImpedanceMaxPercentHasValue)
				{
					rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDNMAXPERCENT, cimPowerTransformer.LeakageImpedanceMaxPercent));
				}
				if (cimPowerTransformer.LeakageImpedanceMinPercentHasValue)
				{
					rd.AddProperty(new Property(ModelCode.WINDINGTEST_LEAKIMPDNMINPERCENT, cimPowerTransformer.LeakageImpedanceMinPercent));
				}
				if (cimPowerTransformer.LoadLossHasValue)
				{
					rd.AddProperty(new Property(ModelCode.WINDINGTEST_LOADLOSS, cimPowerTransformer.LoadLoss));
				}
				if (cimPowerTransformer.NoLoadLossHasValue)
				{
					rd.AddProperty(new Property(ModelCode.WINDINGTEST_NOLOADLOSS, cimPowerTransformer.NoLoadLoss));
				}
				if (cimPowerTransformer.PhaseShiftHasValue)
				{
					rd.AddProperty(new Property(ModelCode.WINDINGTEST_PHASESHIFT, cimPowerTransformer.PhaseShift));
				}
			}
		}
		#endregion Populate ResourceDescription

		#region Enums convert
		public static PhaseCode GetDMSPhaseCode(FTN.PhaseCode phases)
		{
			switch (phases)
			{
				case FTN.PhaseCode.A:
					return PhaseCode.A;
				case FTN.PhaseCode.AB:
					return PhaseCode.AB;
				case FTN.PhaseCode.ABC:
					return PhaseCode.ABC;
				case FTN.PhaseCode.ABCN:
					return PhaseCode.ABCN;
				case FTN.PhaseCode.ABN:
					return PhaseCode.ABN;
				case FTN.PhaseCode.AC:
					return PhaseCode.AC;
				case FTN.PhaseCode.ACN:
					return PhaseCode.ACN;
				case FTN.PhaseCode.AN:
					return PhaseCode.AN;
				case FTN.PhaseCode.B:
					return PhaseCode.B;
				case FTN.PhaseCode.BC:
					return PhaseCode.BC;
				case FTN.PhaseCode.BCN:
					return PhaseCode.BCN;
				case FTN.PhaseCode.BN:
					return PhaseCode.BN;
				case FTN.PhaseCode.C:
					return PhaseCode.C;
				case FTN.PhaseCode.CN:
					return PhaseCode.CN;
				case FTN.PhaseCode.N:
					return PhaseCode.N;
				case FTN.PhaseCode.s12N:
					return PhaseCode.ABN;
				case FTN.PhaseCode.s1N:
					return PhaseCode.AN;
				case FTN.PhaseCode.s2N:
					return PhaseCode.BN;
				default: return PhaseCode.Unknown;
			}
		}

		public static TransformerFunction GetDMSTransformerFunctionKind(FTN.TransformerFunctionKind transformerFunction)
		{
			switch (transformerFunction)
			{
				case FTN.TransformerFunctionKind.voltageRegulator:
					return TransformerFunction.Voltreg;
				default:
					return TransformerFunction.Consumer;
			}
		}

		public static WindingType GetDMSWindingType(FTN.WindingType windingType)
		{
			switch (windingType)
			{
				case FTN.WindingType.primary:
					return WindingType.Primary;
				case FTN.WindingType.secondary:
					return WindingType.Secondary;
				case FTN.WindingType.tertiary:
					return WindingType.Tertiary;
				default:
					return WindingType.None;
			}
		}

		public static WindingConnection GetDMSWindingConnection(FTN.WindingConnection windingConnection)
		{
			switch (windingConnection)
			{
				case FTN.WindingConnection.D:
					return WindingConnection.D;
				case FTN.WindingConnection.I:
					return WindingConnection.I;
				case FTN.WindingConnection.Z:
					return WindingConnection.Z;
				case FTN.WindingConnection.Y:
					return WindingConnection.Y;
				default:
					return WindingConnection.Y;
			}
		}
		#endregion Enums convert
	}
}
