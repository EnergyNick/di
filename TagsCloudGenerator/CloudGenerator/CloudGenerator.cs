﻿using SyntaxTextParser;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TagsCloudGenerator.CloudPrepossessing;

namespace TagsCloudGenerator
{
    public static class CloudGenerator
    { 
        public static List<CloudTag> CreateTagsCloud(List<TextElement> elements, 
            ITagsPrepossessing tagPlacer, CloudFormat cloudFormat)
        {
            elements = cloudFormat.TagOrderPreform.OrderEnumerable(elements);
            var result = new List<CloudTag>();

            foreach (var element in elements)
            {
                var font = new Font(cloudFormat.TagTextFontFamily,
                    Math.Min(cloudFormat.MaximalFontSize, element.Count * cloudFormat.FontSizeMultiplier));
                var size = new Size(TextRenderer.MeasureText(element.Element, font).Width,
                    TextRenderer.MeasureText(element.Element, font).Height);

                var rect = tagPlacer.PutNextRectangle(size);

                result.Add(new CloudTag(rect, element.Element,
                    cloudFormat.TagTextFormat, font));
            }

            return result;
        }
    }
}