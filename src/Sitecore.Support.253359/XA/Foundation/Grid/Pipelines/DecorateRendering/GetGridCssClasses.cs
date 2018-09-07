namespace Sitecore.Support.XA.Foundation.Grid.Pipelines.DecorateRendering
{
  using Microsoft.Extensions.DependencyInjection;
  using Sitecore.Data.Items;
  using Sitecore.DependencyInjection;
  using Sitecore.XA.Foundation.Grid;
  using Sitecore.XA.Foundation.Grid.Model;
  using Sitecore.XA.Foundation.Grid.Parser;
  using Sitecore.XA.Foundation.MarkupDecorator.Pipelines.DecorateRendering;
  using System.Collections.Generic;
  using System.Linq;

  public class GetGridCssClasses : RenderingDecorator
  {
    public override void Process(RenderingDecoratorArgs args)
    {
      Item gridDefinitionItem = ServiceLocator.ServiceProvider.GetService<IGridContext>().GetGridDefinitionItem(args.Rendering.Item, base.Context.Device);
      if (gridDefinitionItem != null)
      {
        IGridFieldParser gridFieldParser = new GridDefinition(gridDefinitionItem).InstantiateGridFieldParser();
        string value = args.Rendering.Parameters["GridParameters"];
        #region Added code
        value = System.Uri.UnescapeDataString(value);
        #endregion
        List<string> list = gridFieldParser.ParseFromFieldValue(value).ToList();
        if (list.Any())
        {
          args.AddAttribute("class", list);
        }
      }
    }
  }
}