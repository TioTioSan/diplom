﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Diplom.SceneHelpers
{
    public static class HelpersGeometry
    {
        public static IndexedVertexModel ControlAxisRotateGeometry = new IndexedVertexModel(
            new Vector3[] { new Vector3(0.8555959f, -0.03825768f, 0.07387591f), new Vector3(0.6341127f, 0.4809631f, -2.745459E-08f), new Vector3(0.8750901f, -0.03482031f, 1.21745E-10f), new Vector3(0.8023366f, -0.04764872f, 0.1279567f), new Vector3(0.6341127f, 0.4809631f, -2.745459E-08f), new Vector3(0.8555959f, -0.03825768f, 0.07387591f), new Vector3(0.7295831f, -0.06047713f, 0.1477517f), new Vector3(0.6341127f, 0.4809631f, -2.745459E-08f), new Vector3(0.8023366f, -0.04764872f, 0.1279567f), new Vector3(0.6568297f, -0.07330552f, 0.1279567f), new Vector3(0.6341127f, 0.4809631f, -2.745459E-08f), new Vector3(0.7295831f, -0.06047713f, 0.1477517f), new Vector3(0.7295831f, -0.06047713f, -0.1477517f), new Vector3(0.6341127f, 0.4809631f, -2.745459E-08f), new Vector3(0.6568297f, -0.07330553f, -0.1279567f), new Vector3(0.8023366f, -0.04764872f, -0.1279567f), new Vector3(0.6341127f, 0.4809631f, -2.745459E-08f), new Vector3(0.7295831f, -0.06047713f, -0.1477517f), new Vector3(0.8555959f, -0.03825768f, -0.07387582f), new Vector3(0.6341127f, 0.4809631f, -2.745459E-08f), new Vector3(0.8023366f, -0.04764872f, -0.1279567f), new Vector3(0.8750901f, -0.03482031f, 1.21745E-10f), new Vector3(0.6341127f, 0.4809631f, -2.745459E-08f), new Vector3(0.8555959f, -0.03825768f, -0.07387582f), new Vector3(0.2666126f, 0.7766161f, 0f), new Vector3(0.2286974f, 0.6661729f, -0.04836776f), new Vector3(0.2221922f, 0.6472238f, -5.979921E-09f), new Vector3(0.2444024f, 0.7119199f, 0.06840233f), new Vector3(0.2601074f, 0.7576669f, 0.04836775f), new Vector3(0.2286974f, 0.6661729f, 0.04836775f), new Vector3(0.8565058f, -0.06866174f, -4.687798E-11f), new Vector3(0.8402017f, -0.07153661f, 0.06178666f), new Vector3(0.8750901f, -0.03482031f, 1.21745E-10f), new Vector3(0.8555959f, -0.03825768f, 0.07387591f), new Vector3(0.8402017f, -0.07153661f, 0.06178666f), new Vector3(0.7956579f, -0.07939088f, 0.1070175f), new Vector3(0.8023366f, -0.04764872f, 0.1279567f), new Vector3(0.73481f, -0.09012f, 0.1235731f), new Vector3(0.7295831f, -0.06047713f, 0.1477517f), new Vector3(0.7956579f, -0.07939088f, 0.1070175f), new Vector3(0.6568297f, -0.07330552f, 0.1279567f), new Vector3(0.6739621f, -0.1008491f, 0.1070175f), new Vector3(0.73481f, -0.09012f, 0.1235731f), new Vector3(0.6739621f, -0.1008491f, 0.1070175f), new Vector3(0.6294184f, -0.1087034f, 0.06178655f), new Vector3(0.6035704f, -0.08269656f, 0.07387579f), new Vector3(0.5840762f, -0.08613393f, -1.994514E-08f), new Vector3(0.6131142f, -0.1115782f, -1.208573E-08f), new Vector3(0.6294184f, -0.1087034f, 0.06178655f), new Vector3(0.6294184f, -0.1087034f, -0.06178657f), new Vector3(0.6035705f, -0.08269657f, -0.07387583f), new Vector3(0.6131142f, -0.1115782f, -1.208573E-08f), new Vector3(0.6739621f, -0.1008491f, -0.1070175f), new Vector3(0.6568297f, -0.07330553f, -0.1279567f), new Vector3(0.6294184f, -0.1087034f, -0.06178657f), new Vector3(0.7295831f, -0.06047713f, -0.1477517f), new Vector3(0.6739621f, -0.1008491f, -0.1070175f), new Vector3(0.73481f, -0.09012f, -0.1235731f), new Vector3(0.8023366f, -0.04764872f, -0.1279567f), new Vector3(0.7956579f, -0.07939088f, -0.1070175f), new Vector3(0.73481f, -0.09012f, -0.1235731f), new Vector3(0.8023366f, -0.04764872f, -0.1279567f), new Vector3(0.7956579f, -0.07939088f, -0.1070175f), new Vector3(0.8555959f, -0.03825768f, -0.07387582f), new Vector3(0.8402017f, -0.07153661f, -0.06178656f), new Vector3(0.8565058f, -0.06866174f, -4.687798E-11f), new Vector3(0.6341127f, 0.4809631f, -2.745459E-08f), new Vector3(0.6341127f, 0.4809631f, -2.745459E-08f), new Vector3(0.6568297f, -0.07330552f, 0.1279567f), new Vector3(0.6035704f, -0.08269656f, 0.07387579f), new Vector3(0.6341127f, 0.4809631f, -2.745459E-08f), new Vector3(0.5840762f, -0.08613393f, -1.994514E-08f), new Vector3(0.6341127f, 0.4809631f, -2.745459E-08f), new Vector3(0.6035705f, -0.08269657f, -0.07387583f), new Vector3(0.6341127f, 0.4809631f, -2.745459E-08f), new Vector3(0.6568297f, -0.07330553f, -0.1279567f), new Vector3(0.73481f, -0.09012f, -0.1235731f), new Vector3(0.6739621f, -0.1008491f, -0.1070175f), new Vector3(0.7956579f, -0.07939088f, -0.1070175f), new Vector3(0.8402017f, -0.07153661f, -0.06178656f), new Vector3(0.6294184f, -0.1087034f, -0.06178657f), new Vector3(0.6131142f, -0.1115782f, -1.208573E-08f), new Vector3(0.8565058f, -0.06866174f, -4.687798E-11f), new Vector3(0.6294184f, -0.1087034f, 0.06178655f), new Vector3(0.8402017f, -0.07153661f, 0.06178666f), new Vector3(0.7956579f, -0.07939088f, 0.1070175f), new Vector3(0.6739621f, -0.1008491f, 0.1070175f), new Vector3(0.73481f, -0.09012f, 0.1235731f), new Vector3(0.2601074f, 0.7576669f, -0.04836776f), new Vector3(0.2286974f, 0.6661729f, -0.04836776f), new Vector3(0.2666126f, 0.7766161f, 0f), new Vector3(0.2444024f, 0.7119199f, -0.06840233f), new Vector3(0.2666126f, 0.7766161f, 0f), new Vector3(0.2221922f, 0.6472238f, -5.979921E-09f), new Vector3(0.2601074f, 0.7576669f, 0.04836775f), new Vector3(0.2286974f, 0.6661729f, 0.04836775f), new Vector3(0.2666126f, 0.7766161f, 0f), new Vector3(-3.589167E-08f, 0.8211058f, 0f), new Vector3(0.2601074f, 0.7576669f, -0.04836776f), new Vector3(-3.501594E-08f, 0.8010713f, -0.04836776f), new Vector3(0.2444024f, 0.7119199f, -0.06840233f), new Vector3(-3.290172E-08f, 0.7527035f, -0.06840233f), new Vector3(-3.078749E-08f, 0.7043357f, -0.04836776f), new Vector3(0.2286974f, 0.6661729f, -0.04836776f), new Vector3(0.2221922f, 0.6472238f, -5.979921E-09f), new Vector3(-2.991175E-08f, 0.6843011f, -5.979921E-09f), new Vector3(-3.078749E-08f, 0.7043357f, 0.04836775f), new Vector3(0.2286974f, 0.6661729f, 0.04836775f), new Vector3(0.2444024f, 0.7119199f, 0.06840233f), new Vector3(-3.290172E-08f, 0.7527035f, 0.06840233f), new Vector3(-3.501594E-08f, 0.8010713f, 0.04836775f), new Vector3(0.2601074f, 0.7576669f, 0.04836775f), new Vector3(-3.589167E-08f, 0.8211058f, 0f), new Vector3(0.2666126f, 0.7766161f, 0f), new Vector3(-3.501594E-08f, 0.8010713f, -0.04836776f), new Vector3(-3.589167E-08f, 0.8211058f, 0f), new Vector3(-0.2601076f, 0.7576669f, -0.04836776f), new Vector3(-0.2666128f, 0.776616f, 0f), new Vector3(-0.4920281f, 0.6321578f, -0.04836776f), new Vector3(-0.46232f, 0.5939888f, -0.06840233f), new Vector3(-0.2444026f, 0.7119199f, -0.06840233f), new Vector3(-3.290172E-08f, 0.7527035f, -0.06840233f), new Vector3(-0.2286976f, 0.6661729f, -0.04836776f), new Vector3(-3.078749E-08f, 0.7043357f, -0.04836776f), new Vector3(-0.5043336f, 0.6479679f, 0f), new Vector3(-0.6874022f, 0.4491023f, 0f), new Vector3(-0.67063f, 0.4381445f, -0.04836776f), new Vector3(-0.6301381f, 0.4116898f, -0.06840233f), new Vector3(-0.5896463f, 0.3852352f, -0.04836776f), new Vector3(-0.4326119f, 0.5558199f, -0.04836776f), new Vector3(-0.7959802f, 0.2015696f, 0f), new Vector3(-0.7765587f, 0.1966514f, -0.04836776f), new Vector3(-0.729671f, 0.1847778f, -0.06840233f), new Vector3(-0.6827832f, 0.1729042f, -0.04836776f), new Vector3(-0.572874f, 0.3742773f, -5.979921E-09f), new Vector3(-0.4203064f, 0.5400098f, -5.979921E-09f), new Vector3(-0.8183013f, -0.06780636f, 0f), new Vector3(-0.7983351f, -0.06615191f, -0.04836776f), new Vector3(-0.7501326f, -0.06215774f, -0.06840233f), new Vector3(-0.7019301f, -0.05816356f, -0.04836776f), new Vector3(-0.6633617f, 0.167986f, -5.979921E-09f), new Vector3(-0.6827832f, 0.1729042f, 0.04836775f), new Vector3(-0.5896463f, 0.3852352f, 0.04836775f), new Vector3(-0.7335998f, -0.3217866f, -0.04836776f), new Vector3(-0.6893058f, -0.3023575f, -0.06840233f), new Vector3(-0.6450118f, -0.2829284f, -0.04836776f), new Vector3(-0.6266648f, -0.2748806f, -5.979921E-09f), new Vector3(-0.6819639f, -0.05650912f, -5.979921E-09f), new Vector3(-0.7019301f, -0.05816356f, 0.04836775f), new Vector3(-0.7519467f, -0.3298344f, 0f), new Vector3(-0.5893673f, -0.5425507f, -0.04836776f), new Vector3(-0.5537819f, -0.5097921f, -0.06840233f), new Vector3(-0.5181966f, -0.4770336f, -0.04836776f), new Vector3(-0.5034567f, -0.4634645f, -5.979921E-09f), new Vector3(-0.6450118f, -0.2829284f, 0.04836775f), new Vector3(-0.6041072f, -0.5561198f, 0f), new Vector3(-0.3908032f, -0.722141f, 0f), new Vector3(-0.3812678f, -0.7045211f, -0.04836776f), new Vector3(-0.3582473f, -0.6619829f, -0.06840233f), new Vector3(-0.3352268f, -0.6194448f, -0.04836776f), new Vector3(-0.3256914f, -0.6018249f, -5.979921E-09f), new Vector3(-0.3352268f, -0.6194448f, 0.04836775f), new Vector3(-0.5181966f, -0.4770336f, 0.04836775f), new Vector3(-0.1351495f, -0.809907f, 0f), new Vector3(-0.1318519f, -0.7901458f, -0.04836776f), new Vector3(-0.1238909f, -0.7424376f, -0.06840233f), new Vector3(-0.1159298f, -0.6947296f, -0.04836776f), new Vector3(-0.1126322f, -0.6749682f, -5.979921E-09f), new Vector3(-0.1159298f, -0.6947296f, 0.04836775f), new Vector3(0.1351496f, -0.809907f, 0f), new Vector3(0.1318521f, -0.7901456f, -0.04836776f), new Vector3(0.123891f, -0.7424375f, -0.06840233f), new Vector3(0.1159299f, -0.6947295f, -0.04836776f), new Vector3(0.1126323f, -0.6749682f, -5.979921E-09f), new Vector3(0.3812678f, -0.7045211f, -0.04836776f), new Vector3(0.3582473f, -0.6619829f, -0.06840233f), new Vector3(0.3352268f, -0.6194448f, -0.04836776f), new Vector3(0.3256914f, -0.6018249f, -5.979921E-09f), new Vector3(0.1159299f, -0.6947295f, 0.04836775f), new Vector3(0.3908032f, -0.722141f, 0f), new Vector3(0.5893673f, -0.5425507f, -0.04836776f), new Vector3(0.553782f, -0.5097921f, -0.06840233f), new Vector3(0.5181966f, -0.4770336f, -0.04836776f), new Vector3(0.5034568f, -0.4634645f, -5.979921E-09f), new Vector3(0.3352268f, -0.6194448f, 0.04836775f), new Vector3(0.123891f, -0.7424375f, 0.06840233f), new Vector3(0.6041072f, -0.5561197f, 0f), new Vector3(0.7519467f, -0.3298344f, 0f), new Vector3(0.7335998f, -0.3217866f, -0.04836776f), new Vector3(0.6893058f, -0.3023575f, -0.06840233f), new Vector3(0.6450118f, -0.2829284f, -0.04836776f), new Vector3(0.6266648f, -0.2748806f, -5.979921E-09f), new Vector3(0.6450118f, -0.2829284f, 0.04836775f), new Vector3(0.5181966f, -0.4770336f, 0.04836775f), new Vector3(0.8183013f, -0.06780633f, 0f), new Vector3(0.7983351f, -0.06615189f, -0.04836776f), new Vector3(0.7501326f, -0.06215771f, -0.06840233f), new Vector3(0.7019301f, -0.05816354f, -0.04836776f), new Vector3(0.6819639f, -0.0565091f, -5.979921E-09f), new Vector3(0.7019301f, -0.05816354f, 0.04836775f), new Vector3(0.6893058f, -0.3023575f, 0.06840233f), new Vector3(0.553782f, -0.5097921f, 0.06840233f), new Vector3(0.3582473f, -0.6619829f, 0.06840233f), new Vector3(0.7501326f, -0.06215771f, 0.06840233f), new Vector3(0.7983351f, -0.06615189f, 0.04836775f), new Vector3(0.7335998f, -0.3217866f, 0.04836775f), new Vector3(0.5893673f, -0.5425507f, 0.04836775f), new Vector3(0.3812678f, -0.7045211f, 0.04836775f), new Vector3(0.1318521f, -0.7901456f, 0.04836775f), new Vector3(-0.1238909f, -0.7424376f, 0.06840233f), new Vector3(0.8183013f, -0.06780633f, 0f), new Vector3(0.7519467f, -0.3298344f, 0f), new Vector3(0.6041072f, -0.5561197f, 0f), new Vector3(0.3908032f, -0.722141f, 0f), new Vector3(0.1351496f, -0.809907f, 0f), new Vector3(-0.1318519f, -0.7901458f, 0.04836775f), new Vector3(-0.3812678f, -0.7045211f, 0.04836775f), new Vector3(-0.3582473f, -0.6619829f, 0.06840233f), new Vector3(-0.5537819f, -0.5097921f, 0.06840233f), new Vector3(-0.1351495f, -0.809907f, 0f), new Vector3(-0.3908032f, -0.722141f, 0f), new Vector3(-0.5893673f, -0.5425507f, 0.04836775f), new Vector3(-0.6041072f, -0.5561198f, 0f), new Vector3(-0.7519467f, -0.3298344f, 0f), new Vector3(-0.7335998f, -0.3217866f, 0.04836775f), new Vector3(-0.6893058f, -0.3023575f, 0.06840233f), new Vector3(-0.7501326f, -0.06215774f, 0.06840233f), new Vector3(-0.729671f, 0.1847778f, 0.06840233f), new Vector3(-0.8183013f, -0.06780636f, 0f), new Vector3(-0.7983351f, -0.06615191f, 0.04836775f), new Vector3(-0.7765587f, 0.1966514f, 0.04836775f), new Vector3(-0.7959802f, 0.2015696f, 0f), new Vector3(-0.67063f, 0.4381445f, 0.04836775f), new Vector3(-0.6301381f, 0.4116898f, 0.06840233f), new Vector3(-0.46232f, 0.5939888f, 0.06840233f), new Vector3(-0.4326119f, 0.5558199f, 0.04836775f), new Vector3(-0.6874022f, 0.4491023f, 0f), new Vector3(-0.4920281f, 0.6321578f, 0.04836775f), new Vector3(-0.2601076f, 0.7576669f, 0.04836775f), new Vector3(-0.2444026f, 0.7119199f, 0.06840233f), new Vector3(-0.2286976f, 0.6661729f, 0.04836775f), new Vector3(-0.2221924f, 0.6472238f, -5.979921E-09f), new Vector3(-0.5043336f, 0.6479679f, 0f), new Vector3(-0.2666128f, 0.776616f, 0f), new Vector3(-3.589167E-08f, 0.8211058f, 0f), new Vector3(-3.501594E-08f, 0.8010713f, 0.04836775f), new Vector3(-3.290172E-08f, 0.7527035f, 0.06840233f), new Vector3(-3.078749E-08f, 0.7043357f, 0.04836775f), new Vector3(-2.991175E-08f, 0.6843011f, -5.979921E-09f) },
            new Vector3[] { new Vector3(0.9059955f, 0.4232872f, -1.371837E-07f), new Vector3(0.9059956f, 0.4232872f, -1.228431E-07f), new Vector3(0.7785772f, 0.40082f, 0.4828673f), new Vector3(0.7785773f, 0.40082f, 0.4828673f), new Vector3(0.7785772f, 0.40082f, 0.4828673f), new Vector3(0.4304637f, 0.3394382f, 0.8363509f), new Vector3(0.4304637f, 0.3394382f, 0.8363509f), new Vector3(0.4304637f, 0.3394382f, 0.8363509f), new Vector3(-0.04506789f, 0.2555891f, 0.9657345f), new Vector3(-0.04506789f, 0.2555891f, 0.9657345f), new Vector3(-0.04506789f, 0.2555891f, 0.9657345f), new Vector3(-0.5205989f, 0.1717402f, 0.8363504f), new Vector3(-0.5205987f, 0.1717401f, -0.8363505f), new Vector3(-0.5205988f, 0.1717401f, -0.8363506f), new Vector3(-0.04506751f, 0.2555891f, -0.9657345f), new Vector3(-0.04506748f, 0.2555891f, -0.9657345f), new Vector3(-0.04506751f, 0.2555891f, -0.9657345f), new Vector3(0.4304642f, 0.3394381f, -0.8363506f), new Vector3(0.4304642f, 0.3394381f, -0.8363506f), new Vector3(0.4304642f, 0.3394381f, -0.8363506f), new Vector3(0.7785773f, 0.4008199f, -0.4828672f), new Vector3(0.7785774f, 0.4008199f, -0.4828672f), new Vector3(0.7785773f, 0.4008199f, -0.4828672f), new Vector3(0.9059956f, 0.4232872f, -1.228431E-07f), new Vector3(0.9458174f, -0.3246992f, -3.694643E-07f), new Vector3(0.9458173f, -0.3246992f, -3.694643E-07f), new Vector3(0.9458174f, -0.3246992f, -3.694643E-07f), new Vector3(0.9458171f, -0.3246998f, 9.610906E-07f), new Vector3(0.9458171f, -0.3246998f, 9.610907E-07f), new Vector3(0.9458172f, -0.3246998f, 9.610907E-07f), new Vector3(0.8765272f, -0.4813523f, -1.317158E-07f), new Vector3(0.7736639f, -0.4994903f, 0.3898124f), new Vector3(0.8765272f, -0.4813523f, -1.081172E-07f), new Vector3(0.7736638f, -0.4994903f, 0.3898124f), new Vector3(0.7736639f, -0.4994903f, 0.3898124f), new Vector3(0.4926363f, -0.5490433f, 0.6751748f), new Vector3(0.4926364f, -0.5490433f, 0.6751747f), new Vector3(0.1087466f, -0.6167336f, 0.7796243f), new Vector3(0.1087462f, -0.6167336f, 0.7796242f), new Vector3(0.4926363f, -0.5490433f, 0.6751748f), new Vector3(-0.2751434f, -0.6844237f, 0.6751741f), new Vector3(-0.2751433f, -0.6844237f, 0.6751743f), new Vector3(0.1087466f, -0.6167336f, 0.7796243f), new Vector3(-0.2751433f, -0.6844237f, 0.6751743f), new Vector3(-0.5561701f, -0.7339765f, 0.389812f), new Vector3(-0.5561701f, -0.7339765f, 0.3898118f), new Vector3(-0.6590332f, -0.7521139f, -1.189289E-07f), new Vector3(-0.6590332f, -0.7521139f, -1.229347E-07f), new Vector3(-0.5561701f, -0.7339765f, 0.389812f), new Vector3(-0.5561704f, -0.7339762f, -0.3898122f), new Vector3(-0.5561703f, -0.7339761f, -0.3898122f), new Vector3(-0.6590332f, -0.7521139f, -1.229347E-07f), new Vector3(-0.2751434f, -0.6844237f, -0.6751744f), new Vector3(-0.2751433f, -0.6844236f, -0.6751744f), new Vector3(-0.5561704f, -0.7339762f, -0.3898122f), new Vector3(0.1087466f, -0.6167334f, -0.7796243f), new Vector3(-0.2751434f, -0.6844237f, -0.6751744f), new Vector3(0.1087466f, -0.6167335f, -0.7796243f), new Vector3(0.4926367f, -0.5490429f, -0.6751748f), new Vector3(0.492637f, -0.5490429f, -0.6751746f), new Vector3(0.1087466f, -0.6167335f, -0.7796243f), new Vector3(0.4926367f, -0.5490429f, -0.6751748f), new Vector3(0.492637f, -0.5490429f, -0.6751746f), new Vector3(0.7736641f, -0.4994899f, -0.3898124f), new Vector3(0.7736642f, -0.4994899f, -0.3898122f), new Vector3(0.8765272f, -0.4813523f, -1.317158E-07f), new Vector3(-0.8687121f, 0.1103585f, 0.4828669f), new Vector3(-0.5205989f, 0.1717402f, 0.8363504f), new Vector3(-0.5205989f, 0.1717402f, 0.8363504f), new Vector3(-0.8687121f, 0.1103585f, 0.4828669f), new Vector3(-0.99613f, 0.08789111f, -3.821545E-07f), new Vector3(-0.99613f, 0.08789112f, -3.787663E-07f), new Vector3(-0.8687118f, 0.1103584f, -0.4828674f), new Vector3(-0.8687118f, 0.1103584f, -0.4828674f), new Vector3(-0.5205987f, 0.1717401f, -0.8363505f), new Vector3(-0.5205988f, 0.1717401f, -0.8363506f), new Vector3(0.1736481f, -0.9848077f, 1.524726E-08f), new Vector3(0.1736481f, -0.9848077f, 1.524726E-08f), new Vector3(0.1736481f, -0.9848077f, 1.524726E-08f), new Vector3(0.1736481f, -0.9848077f, 1.524726E-08f), new Vector3(0.1736481f, -0.9848077f, 1.524726E-08f), new Vector3(0.1736481f, -0.9848078f, 1.524726E-08f), new Vector3(0.1736481f, -0.9848078f, 1.524726E-08f), new Vector3(0.1736481f, -0.9848077f, 1.524726E-08f), new Vector3(0.1736481f, -0.9848077f, 1.524726E-08f), new Vector3(0.1736481f, -0.9848077f, 1.524726E-08f), new Vector3(0.1736481f, -0.9848077f, 1.524726E-08f), new Vector3(0.1736481f, -0.9848077f, 1.524726E-08f), new Vector3(0.9458171f, -0.3246998f, 6.88348E-07f), new Vector3(0.9458171f, -0.3246998f, 6.88348E-07f), new Vector3(0.9458171f, -0.3246998f, 6.88348E-07f), new Vector3(0.9458171f, -0.3246998f, 6.88348E-07f), new Vector3(0.9458173f, -0.3246995f, -3.401E-07f), new Vector3(0.9458172f, -0.3246994f, -3.401E-07f), new Vector3(0.9458172f, -0.3246994f, -3.401E-07f), new Vector3(0.9458173f, -0.3246994f, -3.401E-07f), new Vector3(0.1645946f, 0.9863613f, 6.405578E-08f), new Vector3(-1.380007E-07f, 1f, 5.411793E-08f), new Vector3(0.1202883f, 0.7208481f, -0.682575f), new Vector3(-9.311898E-08f, 0.7261207f, -0.6875673f), new Vector3(0.006711854f, 0.04022167f, -0.9991683f), new Vector3(-1.799099E-08f, 0.04022265f, -0.9991907f), new Vector3(3.013317E-08f, -0.6858391f, -0.7277533f), new Vector3(-0.113704f, -0.6813912f, -0.7230334f), new Vector3(-0.1645945f, -0.9863613f, 0f), new Vector3(8.989642E-08f, -1f, 9.988491E-09f), new Vector3(2.762207E-08f, -0.6858392f, 0.7277532f), new Vector3(-0.1137041f, -0.6813913f, 0.7230334f), new Vector3(0.006711856f, 0.04022165f, 0.9991683f), new Vector3(-1.927606E-08f, 0.04022267f, 0.9991908f), new Vector3(-8.779789E-08f, 0.7261208f, 0.6875672f), new Vector3(0.1202883f, 0.7208481f, 0.682575f), new Vector3(-1.380007E-07f, 1f, 5.411793E-08f), new Vector3(0.1645946f, 0.9863613f, 6.405578E-08f), new Vector3(-9.311898E-08f, 0.7261207f, -0.6875673f), new Vector3(-1.380007E-07f, 1f, 5.411793E-08f), new Vector3(-0.2357711f, 0.6867775f, -0.6875672f), new Vector3(-0.3246994f, 0.9458172f, 3.247076E-08f), new Vector3(-0.4459926f, 0.5730113f, -0.6875672f), new Vector3(-0.02470523f, 0.03174134f, -0.9991907f), new Vector3(-0.01306031f, 0.03804342f, -0.9991908f), new Vector3(-1.799099E-08f, 0.04022265f, -0.9991907f), new Vector3(0.2226916f, -0.6486784f, -0.7277532f), new Vector3(3.013317E-08f, -0.6858391f, -0.7277533f), new Vector3(-0.6142127f, 0.7891405f, 1.082359E-08f), new Vector3(-0.8371665f, 0.5469482f, 2.164717E-08f), new Vector3(-0.6078842f, 0.3971505f, -0.6875671f), new Vector3(-0.03367296f, 0.02199964f, -0.9991908f), new Vector3(0.5741615f, -0.3751186f, -0.7277532f), new Vector3(0.4212511f, -0.5412235f, -0.7277531f), new Vector3(-0.9694002f, 0.2454855f, 3.247075E-08f), new Vector3(-0.7039018f, 0.1782521f, -0.6875671f), new Vector3(-0.03899198f, 0.009874066f, -0.9991908f), new Vector3(0.6648525f, -0.1683635f, -0.7277533f), new Vector3(0.8371665f, -0.5469482f, -9.98849E-09f), new Vector3(0.6142127f, -0.7891406f, -9.98849E-09f), new Vector3(-0.9965845f, -0.08257929f, 4.329434E-08f), new Vector3(-0.7236408f, -0.05996253f, -0.6875671f), new Vector3(-0.04008535f, -0.003321519f, -0.9991908f), new Vector3(0.6834964f, 0.05663615f, -0.7277533f), new Vector3(0.9694002f, -0.2454854f, 0f), new Vector3(0.6648526f, -0.1683635f, 0.7277532f), new Vector3(0.5741615f, -0.3751186f, 0.7277531f), new Vector3(-0.6649622f, -0.2916794f, -0.687567f), new Vector3(-0.03683478f, -0.01615721f, -0.9991908f), new Vector3(0.6280731f, 0.2754985f, -0.7277532f), new Vector3(0.9157734f, 0.4016954f, 9.98849E-09f), new Vector3(0.9965844f, 0.0825794f, 0f), new Vector3(0.6834965f, 0.05663615f, 0.7277533f), new Vector3(-0.9157733f, -0.4016955f, 7.576507E-08f), new Vector3(-0.5342244f, -0.4917881f, -0.6875673f), new Vector3(-0.02959257f, -0.02724189f, -0.9991908f), new Vector3(0.5045884f, 0.4645062f, -0.727753f), new Vector3(0.735724f, 0.6772815f, 0f), new Vector3(0.6280732f, 0.2754986f, 0.727753f), new Vector3(-0.735724f, -0.6772814f, 6.494152E-08f), new Vector3(-0.4759474f, -0.8794737f, 6.494152E-08f), new Vector3(-0.3455953f, -0.6386039f, -0.6875675f), new Vector3(-0.01914371f, -0.03537445f, -0.9991908f), new Vector3(0.3264235f, 0.6031776f, -0.727753f), new Vector3(0.4759476f, 0.8794737f, 0f), new Vector3(0.3264236f, 0.6031777f, 0.7277529f), new Vector3(0.5045884f, 0.4645063f, 0.727753f), new Vector3(-0.1645945f, -0.9863613f, 3.247075E-08f), new Vector3(-0.1195154f, -0.7162174f, -0.6875672f), new Vector3(-0.006620373f, -0.03967379f, -0.9991908f), new Vector3(0.1128854f, 0.6764852f, -0.7277531f), new Vector3(0.1645945f, 0.9863613f, 9.988488E-09f), new Vector3(0.1128854f, 0.6764854f, 0.727753f), new Vector3(0.1645947f, -0.9863613f, 3.247076E-08f), new Vector3(0.1195156f, -0.7162173f, -0.6875673f), new Vector3(0.006620397f, -0.03967382f, -0.9991907f), new Vector3(-0.1128855f, 0.6764853f, -0.7277529f), new Vector3(-0.1645946f, 0.9863612f, 9.988488E-09f), new Vector3(0.3455953f, -0.638604f, -0.6875673f), new Vector3(0.01914376f, -0.03537461f, -0.9991908f), new Vector3(-0.3264235f, 0.6031777f, -0.7277529f), new Vector3(-0.4759475f, 0.8794736f, 0f), new Vector3(-0.1128855f, 0.6764854f, 0.7277529f), new Vector3(0.4759474f, -0.8794737f, 6.494152E-08f), new Vector3(0.5342246f, -0.4917883f, -0.6875671f), new Vector3(0.02959256f, -0.02724191f, -0.9991907f), new Vector3(-0.5045884f, 0.4645063f, -0.7277529f), new Vector3(-0.7357241f, 0.6772813f, 0f), new Vector3(-0.3264236f, 0.6031778f, 0.7277529f), new Vector3(0.006620401f, -0.03967384f, 0.9991908f), new Vector3(0.7357241f, -0.6772814f, 6.494149E-08f), new Vector3(0.9157733f, -0.4016955f, 5.411789E-08f), new Vector3(0.6649624f, -0.2916794f, -0.6875669f), new Vector3(0.03683472f, -0.01615719f, -0.9991908f), new Vector3(-0.6280732f, 0.2754985f, -0.7277531f), new Vector3(-0.9157733f, 0.4016954f, 0f), new Vector3(-0.6280733f, 0.2754985f, 0.727753f), new Vector3(-0.5045885f, 0.4645064f, 0.7277529f), new Vector3(0.9694002f, -0.2454853f, 8.540772E-08f), new Vector3(0.7084532f, -0.1794045f, -0.6825746f), new Vector3(0.03953008f, -0.01001027f, -0.9991682f), new Vector3(-0.6696741f, 0.1695845f, -0.7230335f), new Vector3(-0.9694003f, 0.2454855f, 0f), new Vector3(-0.6696742f, 0.1695846f, 0.7230335f), new Vector3(0.03683477f, -0.01615721f, 0.9991908f), new Vector3(0.0295926f, -0.02724192f, 0.9991908f), new Vector3(0.01914376f, -0.03537461f, 0.9991907f), new Vector3(0.03953006f, -0.01001028f, 0.9991683f), new Vector3(0.7084532f, -0.1794046f, 0.6825746f), new Vector3(0.6649624f, -0.2916794f, 0.6875668f), new Vector3(0.5342246f, -0.4917882f, 0.687567f), new Vector3(0.3455953f, -0.638604f, 0.6875673f), new Vector3(0.1195156f, -0.7162173f, 0.6875672f), new Vector3(-0.006620373f, -0.0396738f, 0.9991908f), new Vector3(0.9694002f, -0.2454853f, 8.540772E-08f), new Vector3(0.9157733f, -0.4016955f, 5.411789E-08f), new Vector3(0.7357241f, -0.6772814f, 6.494149E-08f), new Vector3(0.4759474f, -0.8794737f, 6.494152E-08f), new Vector3(0.1645947f, -0.9863613f, 3.247076E-08f), new Vector3(-0.1195154f, -0.7162175f, 0.6875672f), new Vector3(-0.3455953f, -0.6386039f, 0.6875674f), new Vector3(-0.01914372f, -0.03537444f, 0.9991908f), new Vector3(-0.02959258f, -0.02724191f, 0.9991907f), new Vector3(-0.1645945f, -0.9863613f, 3.247075E-08f), new Vector3(-0.4759474f, -0.8794737f, 6.494152E-08f), new Vector3(-0.5342244f, -0.4917881f, 0.6875673f), new Vector3(-0.735724f, -0.6772814f, 6.494152E-08f), new Vector3(-0.9157733f, -0.4016955f, 7.576507E-08f), new Vector3(-0.6649622f, -0.2916794f, 0.6875669f), new Vector3(-0.03683475f, -0.01615722f, 0.9991908f), new Vector3(-0.04008533f, -0.003321527f, 0.9991907f), new Vector3(-0.03899201f, 0.009874073f, 0.9991908f), new Vector3(-0.9965845f, -0.08257929f, 4.329434E-08f), new Vector3(-0.7236408f, -0.05996254f, 0.6875671f), new Vector3(-0.7039018f, 0.1782521f, 0.6875671f), new Vector3(-0.9694002f, 0.2454855f, 3.247075E-08f), new Vector3(-0.6078841f, 0.3971505f, 0.687567f), new Vector3(-0.03367299f, 0.02199966f, 0.9991907f), new Vector3(-0.02470526f, 0.03174135f, 0.9991907f), new Vector3(0.4212511f, -0.5412235f, 0.727753f), new Vector3(-0.8371665f, 0.5469482f, 2.164717E-08f), new Vector3(-0.4459926f, 0.5730113f, 0.6875672f), new Vector3(-0.2357711f, 0.6867775f, 0.6875672f), new Vector3(-0.01306031f, 0.03804342f, 0.9991907f), new Vector3(0.2226916f, -0.6486784f, 0.7277532f), new Vector3(0.3246996f, -0.9458172f, 9.988489E-09f), new Vector3(-0.6142127f, 0.7891405f, 1.082359E-08f), new Vector3(-0.3246994f, 0.9458172f, 3.247076E-08f), new Vector3(-1.380007E-07f, 1f, 5.411793E-08f), new Vector3(-8.779789E-08f, 0.7261208f, 0.6875672f), new Vector3(-1.927606E-08f, 0.04022267f, 0.9991908f), new Vector3(2.762207E-08f, -0.6858392f, 0.7277532f), new Vector3(8.989642E-08f, -1f, 9.988491E-09f) },
            new short[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 32, 31, 34, 35, 33, 36, 33, 35, 37, 38, 39, 36, 39, 38, 40, 38, 41, 42, 41, 38, 43, 44, 40, 45, 40, 44, 46, 45, 47, 48, 47, 45, 49, 50, 51, 51, 50, 46, 52, 53, 54, 50, 54, 53, 55, 53, 56, 55, 56, 57, 58, 55, 59, 60, 59, 55, 61, 62, 63, 64, 63, 62, 63, 64, 32, 64, 65, 32, 66, 67, 68, 66, 68, 69, 70, 66, 69, 70, 69, 71, 72, 70, 71, 72, 71, 73, 74, 72, 73, 74, 73, 75, 76, 77, 78, 79, 78, 77, 80, 79, 77, 80, 81, 79, 79, 81, 82, 81, 83, 82, 84, 82, 83, 84, 83, 85, 86, 85, 83, 87, 85, 86, 88, 89, 90, 91, 89, 88, 92, 93, 94, 95, 94, 93, 96, 97, 98, 99, 98, 97, 98, 99, 100, 101, 100, 99, 102, 100, 101, 103, 100, 102, 103, 102, 104, 105, 104, 102, 106, 104, 105, 104, 106, 107, 107, 106, 108, 109, 108, 106, 110, 108, 109, 108, 110, 111, 110, 112, 111, 113, 111, 112, 114, 115, 116, 117, 116, 115, 116, 117, 118, 118, 119, 116, 116, 119, 120, 120, 114, 116, 114, 120, 121, 122, 121, 120, 121, 122, 123, 124, 118, 117, 124, 125, 118, 126, 118, 125, 126, 127, 118, 118, 127, 119, 127, 128, 119, 119, 128, 129, 129, 122, 119, 120, 119, 122, 125, 130, 126, 131, 126, 130, 131, 132, 126, 127, 126, 132, 132, 133, 127, 128, 127, 133, 128, 133, 134, 134, 135, 128, 129, 128, 135, 135, 122, 129, 130, 136, 131, 137, 131, 136, 131, 137, 132, 138, 132, 137, 139, 132, 138, 133, 132, 139, 133, 139, 140, 140, 134, 133, 141, 134, 140, 134, 141, 142, 142, 135, 134, 137, 136, 143, 137, 143, 138, 144, 138, 143, 138, 144, 139, 145, 139, 144, 146, 139, 145, 139, 146, 147, 147, 140, 139, 148, 140, 147, 140, 148, 141, 149, 143, 136, 143, 149, 150, 143, 150, 144, 151, 144, 150, 144, 151, 145, 152, 145, 151, 153, 145, 152, 145, 153, 146, 146, 153, 154, 154, 148, 146, 147, 146, 148, 155, 150, 149, 155, 156, 150, 157, 150, 156, 150, 157, 151, 158, 151, 157, 151, 158, 159, 151, 159, 152, 152, 159, 153, 160, 153, 159, 161, 153, 160, 153, 161, 162, 162, 154, 153, 156, 163, 157, 164, 157, 163, 164, 165, 157, 157, 165, 158, 165, 166, 158, 158, 166, 159, 159, 166, 160, 167, 160, 166, 168, 160, 167, 160, 168, 161, 163, 169, 164, 170, 164, 169, 164, 170, 165, 171, 165, 170, 165, 171, 166, 172, 166, 171, 173, 166, 172, 166, 173, 167, 167, 173, 168, 170, 169, 174, 170, 174, 171, 175, 171, 174, 176, 171, 175, 172, 171, 176, 177, 172, 176, 172, 177, 173, 173, 177, 178, 178, 168, 173, 179, 174, 169, 174, 179, 180, 174, 180, 175, 181, 175, 180, 182, 175, 181, 176, 175, 182, 183, 176, 182, 176, 183, 177, 177, 183, 184, 184, 178, 177, 178, 184, 185, 178, 185, 168, 186, 180, 179, 186, 187, 180, 188, 180, 187, 189, 180, 188, 181, 180, 189, 190, 181, 189, 182, 181, 190, 182, 190, 183, 191, 183, 190, 192, 183, 191, 183, 192, 193, 193, 184, 183, 187, 194, 188, 195, 188, 194, 188, 195, 189, 196, 189, 195, 197, 189, 196, 189, 197, 190, 190, 197, 191, 198, 191, 197, 199, 191, 198, 191, 199, 192, 192, 199, 200, 200, 201, 192, 193, 192, 201, 201, 202, 193, 184, 193, 202, 202, 185, 184, 203, 200, 199, 204, 200, 203, 200, 204, 205, 205, 206, 200, 201, 200, 206, 202, 201, 206, 202, 206, 207, 202, 207, 185, 185, 207, 208, 185, 208, 209, 209, 168, 185, 204, 210, 205, 211, 205, 210, 205, 211, 206, 212, 206, 211, 212, 213, 206, 207, 206, 213, 213, 214, 207, 208, 207, 214, 208, 214, 215, 215, 209, 208, 215, 216, 209, 217, 209, 216, 209, 217, 168, 161, 168, 217, 217, 218, 161, 162, 161, 218, 218, 154, 162, 219, 215, 214, 215, 219, 216, 220, 216, 219, 216, 220, 221, 221, 218, 216, 217, 216, 218, 222, 221, 220, 222, 223, 221, 224, 221, 223, 225, 221, 224, 221, 225, 218, 154, 218, 225, 225, 148, 154, 148, 225, 226, 226, 227, 148, 141, 148, 227, 223, 228, 224, 229, 224, 228, 226, 224, 229, 224, 226, 225, 229, 228, 230, 227, 229, 230, 226, 229, 227, 231, 230, 228, 230, 231, 232, 230, 232, 227, 233, 227, 232, 227, 233, 141, 142, 141, 233, 233, 234, 142, 235, 142, 234, 135, 142, 235, 236, 232, 231, 232, 236, 237, 232, 237, 233, 234, 233, 237, 237, 238, 234, 239, 234, 238, 239, 240, 234, 235, 234, 240, 235, 240, 135, 241, 135, 240, 122, 135, 241, 241, 123, 122, 242, 237, 236, 242, 243, 237, 238, 237, 243, 243, 244, 238, 245, 238, 244, 238, 245, 239, 246, 239, 245, 239, 246, 240, 247, 240, 246, 240, 247, 241, 248, 241, 247, 123, 241, 248 });

        public static IndexedVertexModel ControlAxisTranslateGeomerty = new IndexedVertexModel(
            new Vector3[] { new Vector3(0f, 0f, 0.9f), new Vector3(-8.742278E-09f, 0.2f, 0f), new Vector3(0.09999999f, 0.1732051f, 0f), new Vector3(0f, 0f, 0.9f), new Vector3(-0.1f, 0.1732051f, 0f), new Vector3(0f, 0f, 0.9f), new Vector3(-0.1732051f, 0.1f, 0f), new Vector3(0f, 0f, 0.9f), new Vector3(-0.2f, 3.019916E-08f, 0f), new Vector3(0f, 0f, 0.9f), new Vector3(-0.1732051f, -0.09999995f, 0f), new Vector3(0f, 0f, 0.9f), new Vector3(-0.1000001f, -0.173205f, 0f), new Vector3(0f, 0f, 0.9f), new Vector3(-9.298245E-08f, -0.2f, 0f), new Vector3(0f, 0f, 0.9f), new Vector3(0.0999999f, -0.1732051f, 0f), new Vector3(0f, 0f, 0.9f), new Vector3(0.173205f, -0.1000001f, 0f), new Vector3(0f, 0f, 0.9f), new Vector3(0.2f, 0f, 0f), new Vector3(0f, 0f, 0.9f), new Vector3(0.1732051f, 0.09999999f, 0f), new Vector3(0f, 0f, 0.9f), new Vector3(0.09999999f, 0.1732051f, 0f), new Vector3(0.09999999f, 0.1732051f, 0f), new Vector3(-8.742278E-09f, 0.2f, 0f), new Vector3(0f, 0f, 0f), new Vector3(-0.1f, 0.1732051f, 0f), new Vector3(-0.1732051f, 0.1f, 0f), new Vector3(-0.2f, 3.019916E-08f, 0f), new Vector3(-0.1732051f, -0.09999995f, 0f), new Vector3(-0.1000001f, -0.173205f, 0f), new Vector3(-9.298245E-08f, -0.2f, 0f), new Vector3(0.0999999f, -0.1732051f, 0f), new Vector3(0.173205f, -0.1000001f, 0f), new Vector3(0.2f, 0f, 0f), new Vector3(0.1732051f, 0.09999999f, 0f), new Vector3(0.09999999f, 0.1732051f, 0f), new Vector3(0f, 0f, 0.9f), new Vector3(0f, 0f, 0.9f), new Vector3(0f, 0f, 0.9f), new Vector3(0f, 0f, 0.9f), new Vector3(0f, 0f, 0.9f), new Vector3(0f, 0f, 0.9f), new Vector3(0f, 0f, 0.9f), new Vector3(0f, 0f, 0.9f), new Vector3(0f, 0f, 0.9f), new Vector3(0f, 0f, 0.9f), new Vector3(0f, 0f, 0.9f), new Vector3(0f, 0f, 0.9f), new Vector3(0f, 0f, 0.9f) },
            new Vector3[] { new Vector3(0.2530548f, 0.9444141f, 0.2098698f), new Vector3(-1.118612E-07f, 0.9761871f, 0.2169304f), new Vector3(0.4880935f, 0.8454028f, 0.2169304f), new Vector3(-0.2530551f, 0.944414f, 0.2098698f), new Vector3(-0.4880936f, 0.8454028f, 0.2169304f), new Vector3(-0.691359f, 0.691359f, 0.2098698f), new Vector3(-0.8454027f, 0.4880936f, 0.2169304f), new Vector3(-0.944414f, 0.2530551f, 0.2098698f), new Vector3(-0.976187f, 2.033839E-07f, 0.2169304f), new Vector3(-0.9444141f, -0.2530547f, 0.2098698f), new Vector3(-0.8454029f, -0.4880932f, 0.2169304f), new Vector3(-0.6913593f, -0.6913588f, 0.2098698f), new Vector3(-0.4880938f, -0.8454026f, 0.2169304f), new Vector3(-0.2530552f, -0.944414f, 0.2098698f), new Vector3(-4.169371E-07f, -0.976187f, 0.2169304f), new Vector3(0.2530544f, -0.9444142f, 0.2098698f), new Vector3(0.488093f, -0.8454031f, 0.2169304f), new Vector3(0.6913587f, -0.6913594f, 0.2098698f), new Vector3(0.8454025f, -0.4880939f, 0.2169304f), new Vector3(0.944414f, -0.2530553f, 0.2098698f), new Vector3(0.9761871f, -1.220304E-07f, 0.2169304f), new Vector3(0.944414f, 0.2530551f, 0.2098698f), new Vector3(0.8454028f, 0.4880935f, 0.2169304f), new Vector3(0.6913592f, 0.6913589f, 0.2098698f), new Vector3(0.4880935f, 0.8454028f, 0.2169304f), new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, -1f), new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f) },
            new short[] { 0, 1, 2, 0, 3, 1, 3, 4, 1, 3, 5, 4, 5, 6, 4, 5, 7, 6, 7, 8, 6, 7, 9, 8, 9, 10, 8, 9, 11, 10, 11, 12, 10, 11, 13, 12, 13, 14, 12, 13, 15, 14, 15, 16, 14, 15, 17, 16, 17, 18, 16, 17, 19, 18, 19, 20, 18, 19, 21, 20, 21, 22, 20, 21, 23, 22, 23, 24, 22, 23, 0, 24, 25, 26, 27, 26, 28, 27, 28, 29, 27, 29, 30, 27, 30, 31, 27, 31, 32, 27, 32, 33, 27, 33, 34, 27, 34, 35, 27, 35, 36, 27, 36, 37, 27, 37, 38, 27, 39, 40, 41, 40, 42, 41, 42, 43, 41, 43, 44, 41, 44, 45, 41, 45, 46, 41, 46, 47, 41, 47, 48, 41, 48, 49, 41, 49, 50, 41, 50, 51, 41, 51, 39, 41 });

        public static IndexedVertexModel ControlAxisScaleGeomerty = new IndexedVertexModel(
            new Vector3[] { new Vector3(-0.4519059f, 0.4519059f, 0f), new Vector3(-0.4519059f, 0.4519059f, 0.9038118f), new Vector3(-0.4519059f, -0.4519059f, 0.9038118f), new Vector3(-0.4519059f, -0.4519059f, 0f), new Vector3(0.4519059f, 0.4519059f, 0f), new Vector3(0.4519059f, 0.4519059f, 0.9038118f), new Vector3(-0.4519059f, 0.4519059f, 0.9038118f), new Vector3(-0.4519059f, 0.4519059f, 0f), new Vector3(0.4519059f, -0.4519059f, 0f), new Vector3(0.4519059f, -0.4519059f, 0.9038118f), new Vector3(0.4519059f, 0.4519059f, 0.9038118f), new Vector3(0.4519059f, 0.4519059f, 0f), new Vector3(-0.4519059f, -0.4519059f, 0f), new Vector3(-0.4519059f, -0.4519059f, 0.9038118f), new Vector3(0.4519059f, -0.4519059f, 0.9038118f), new Vector3(0.4519059f, -0.4519059f, 0f), new Vector3(-0.4519059f, -0.4519059f, 0.9038118f), new Vector3(-0.4519059f, 0.4519059f, 0.9038118f), new Vector3(0.4519059f, 0.4519059f, 0.9038118f), new Vector3(0.4519059f, -0.4519059f, 0.9038118f), new Vector3(-0.4519059f, -0.4519059f, 0f), new Vector3(0.4519059f, -0.4519059f, 0f), new Vector3(0.4519059f, 0.4519059f, 0f), new Vector3(-0.4519059f, 0.4519059f, 0f) },
            new Vector3[] { new Vector3(-1f, 0f, 0f), new Vector3(-1f, 0f, 0f), new Vector3(-1f, 0f, 0f), new Vector3(-1f, 0f, 0f), new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(0f, 1f, 0f), new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(1f, 0f, 0f), new Vector3(0f, -1f, 0f), new Vector3(0f, -1f, 0f), new Vector3(0f, -1f, 0f), new Vector3(0f, -1f, 0f), new Vector3(0f, 0f, 1f), new Vector3(0f, 0f, 1f), new Vector3(0f, 0f, 1f), new Vector3(0f, 0f, 1f), new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, -1f), new Vector3(0f, 0f, -1f) },
            new short[] { 0, 1, 2, 2, 3, 0, 4, 5, 6, 6, 7, 4, 8, 9, 10, 10, 11, 8, 12, 13, 14, 14, 15, 12, 16, 17, 18, 18, 19, 16, 20, 21, 22, 22, 23, 20 });
    }
}
