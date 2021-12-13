using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DomaciPR44_2018
{
    class Program
    {
        static void Main(string[] args)
        {
            Delta delta = new Delta();
            //BaseVoltage1
            ResourceDescription baseVoltage1 = new ResourceDescription()
            {
                Id = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.BASEVOLTAGE, -1),
                Properties = new List<Property>()
                {
                    new Property(ModelCode.IDOBJ_NAME,"BaseVoltage1"),
                    new Property(ModelCode.IDOBJ_MRID,"BaseVoltage1MRD"),
                    new Property(ModelCode.IDOBJ_DESCRIPTION,"DescriptionBaseVoltage1"),
                    new Property(ModelCode.BASEVOLTAGE_NOMINALVOLTAGE,110f)
                }
            };

            //BaseVoltage2
            ResourceDescription baseVoltage2 = new ResourceDescription()
            {
                Id = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.BASEVOLTAGE, -1),
                Properties = new List<Property>()
                {
                    new Property(ModelCode.IDOBJ_NAME,"BaseVoltage2"),
                    new Property(ModelCode.IDOBJ_MRID,"BaseVoltage2MRD"),
                    new Property(ModelCode.IDOBJ_DESCRIPTION,"DescriptionBaseVoltage2"),
                    new Property(ModelCode.BASEVOLTAGE_NOMINALVOLTAGE,20f)
                }
            };
            //Location
            ResourceDescription location = new ResourceDescription()
            {
                Id = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.LOCATION, -1),
                Properties = new List<Property>()
                {
                    new Property(ModelCode.IDOBJ_NAME,"Location1"),
                    new Property(ModelCode.IDOBJ_MRID,"LocationMRID"),
                    new Property(ModelCode.IDOBJ_DESCRIPTION,"DescriptionLocation1"),
                    new Property(ModelCode.LOCATION_CORPORATECODE, "Corporate_code"),
                    new Property(ModelCode.LOCATION_CATEGORY, "Category")
                }
            };
            //PowerTransformer
     
            ResourceDescription powerTrasformer = new ResourceDescription()
            {
                Id = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.POWERTR, -1),
                Properties = new List<Property>()
                {
                    new Property(ModelCode.IDOBJ_NAME,"PowerTransformer1"),
                    new Property(ModelCode.IDOBJ_MRID,"PowerTransformer1MRID"),
                    new Property(ModelCode.IDOBJ_DESCRIPTION,"DescriptionPowerTransformer1"),
                    new Property(ModelCode.PSR_LOCATION,location.Id),
                    new Property(ModelCode.EQUIPMENT_ISPRIVATE, true),
                    new Property(ModelCode.EQUIPMENT_ISUNDERGROUND, false),
                    new Property(ModelCode.POWERTR_FUNC,(short) TransformerFunction.Generator),
                    new Property(ModelCode.POWERTR_AUTO,false),
                }
            };
            
            //TransformerWinding
            ResourceDescription transformerWinding = new ResourceDescription()
            {
                Id = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.POWERTRWINDING, -1),
                Properties = new List<Property>()
                {
                    new Property(ModelCode.IDOBJ_NAME,"TransformerWinding1"),
                    new Property(ModelCode.IDOBJ_MRID,"TransformerWinding1MRID"),
                    new Property(ModelCode.IDOBJ_DESCRIPTION,"DescriptionTransformerWinding1"),
                    new Property(ModelCode.POWERTRWINDING_POWERTRW,powerTrasformer.Id),
                    new Property(ModelCode.POWERTRWINDING_CONNTYPE,(short)WindingConnection.D),
                    new Property(ModelCode.POWERTRWINDING_GROUNDED,false),
                    new Property(ModelCode.POWERTRWINDING_RATEDS,2.44f),
                    new Property(ModelCode.POWERTRWINDING_RATEDU,3.44f),
                    new Property(ModelCode.POWERTRWINDING_WINDTYPE,(short)WindingType.Secondary),
                    new Property(ModelCode.POWERTRWINDING_PHASETOGRNDVOLTAGE,3.33f),
                    new Property(ModelCode.POWERTRWINDING_PHASETOPHASEVOLTAGE,3.44f),
                    new Property(ModelCode.CONDEQ_BASVOLTAGE,(long)baseVoltage1.Id),
                    new Property(ModelCode.CONDEQ_PHASES,(short)PhaseCode.ABC),
                    new Property(ModelCode.CONDEQ_RATEDVOLTAGE,3.44f),
                }
            };


            //TransformerWinding1
            ResourceDescription transformerWinding1= new ResourceDescription()
            {
                Id = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.POWERTRWINDING, -1),
                Properties = new List<Property>()
                {
                    new Property(ModelCode.IDOBJ_NAME,"TransformerWinding2"),
                    new Property(ModelCode.IDOBJ_MRID,"TransformerWinding2MRID"),
                    new Property(ModelCode.IDOBJ_DESCRIPTION,"DescriptionTransformerWinding2"),
                    new Property(ModelCode.POWERTRWINDING_POWERTRW,powerTrasformer.Id),
                    new Property(ModelCode.POWERTRWINDING_CONNTYPE,(short)WindingConnection.D),
                    new Property(ModelCode.POWERTRWINDING_GROUNDED,false),
                    new Property(ModelCode.POWERTRWINDING_RATEDS,2.44f),
                    new Property(ModelCode.POWERTRWINDING_RATEDU,3.44f),
                    new Property(ModelCode.POWERTRWINDING_WINDTYPE,(short)WindingType.Secondary),
                    new Property(ModelCode.POWERTRWINDING_PHASETOGRNDVOLTAGE,3.33f),
                    new Property(ModelCode.POWERTRWINDING_PHASETOPHASEVOLTAGE,3.44f),
                    new Property(ModelCode.CONDEQ_BASVOLTAGE,(long)baseVoltage2.Id),
                    new Property(ModelCode.CONDEQ_PHASES,(long)PhaseCode.ABC),
                    new Property(ModelCode.CONDEQ_RATEDVOLTAGE,3.44f),
                }
            };

            ResourceDescription windingTest = new ResourceDescription()
            {
                Id = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.WINDINGTEST, -1),
                Properties = new List<Property>
            {
                new Property(ModelCode.IDOBJ_NAME, "WindingTest1"),
                new Property(ModelCode.IDOBJ_MRID, "WindingTest1MRID2"),
                new Property(ModelCode.IDOBJ_DESCRIPTION, "WindingTest1Description"),
                new Property(ModelCode.WINDINGTEST_LEAKIMPDN, 2f),
                new Property(ModelCode.WINDINGTEST_LEAKIMPDN0PERCENT, 1f),
                new Property(ModelCode.WINDINGTEST_LEAKIMPDNMAXPERCENT, 5f),
                new Property(ModelCode.WINDINGTEST_LEAKIMPDNMINPERCENT, 0.5f),
                new Property(ModelCode.WINDINGTEST_LOADLOSS, 2f),
                new Property(ModelCode.WINDINGTEST_NOLOADLOSS, 118f),
                new Property(ModelCode.WINDINGTEST_PHASESHIFT, 2f),
                new Property(ModelCode.WINDINGTEST_POWERTRWINDING, transformerWinding.Id)
            }
            };
            ResourceDescription windingTest1 = new ResourceDescription()
            {
                Id = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.WINDINGTEST, -1),
                Properties = new List<Property>
            {
                new Property(ModelCode.IDOBJ_NAME, "WindingTest2"),
                new Property(ModelCode.IDOBJ_MRID, "WindingTest1MRID2"),
                new Property(ModelCode.IDOBJ_DESCRIPTION, "WindingTest2Description"),
                new Property(ModelCode.WINDINGTEST_LEAKIMPDN, 2f),
                new Property(ModelCode.WINDINGTEST_LEAKIMPDN0PERCENT, 1f),
                new Property(ModelCode.WINDINGTEST_LEAKIMPDNMAXPERCENT, 5f),
                new Property(ModelCode.WINDINGTEST_LEAKIMPDNMINPERCENT, 0.5f),
                new Property(ModelCode.WINDINGTEST_LOADLOSS, 2f),
                new Property(ModelCode.WINDINGTEST_NOLOADLOSS, 118f),
                new Property(ModelCode.WINDINGTEST_PHASESHIFT, 2f),
                new Property(ModelCode.WINDINGTEST_POWERTRWINDING, transformerWinding1.Id)
            }
            };
            Console.WriteLine("Created now add to delta");
            delta.AddDeltaOperation(DeltaOpType.Insert, baseVoltage1, true);
            delta.AddDeltaOperation(DeltaOpType.Insert, baseVoltage2, true);
            delta.AddDeltaOperation(DeltaOpType.Insert, location, true);
            delta.AddDeltaOperation(DeltaOpType.Insert, powerTrasformer, true);
            delta.AddDeltaOperation(DeltaOpType.Insert, transformerWinding, true);
            delta.AddDeltaOperation(DeltaOpType.Insert, transformerWinding1, true);
            delta.AddDeltaOperation(DeltaOpType.Insert, windingTest, true);
            delta.AddDeltaOperation(DeltaOpType.Insert, windingTest1, true);
            Console.WriteLine("Added to delta");
            Console.WriteLine("Xml adding ");

            XmlTextWriter xmlTextWriter = new XmlTextWriter("myXmFile.xml", null);
            xmlTextWriter.WriteStartDocument();
            delta.ExportToXml(xmlTextWriter);

            xmlTextWriter.WriteEndDocument();
            xmlTextWriter.Close();
            Console.WriteLine("Xml eneded");
            Console.ReadLine();

        }
    }
}
