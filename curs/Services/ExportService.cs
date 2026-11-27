using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace curs.Services;

/// <summary>
/// сохраняет элементы окна в файл
/// </summary>
public sealed class ExportService
{
	/// <summary>
	/// сохраняет элемент окна в формате png
	/// </summary>
	/// <param name="element">элемент для сохранения</param>
	/// <param name="filePath">путь к файлу</param>
	public void ExportPng(FrameworkElement element, string filePath)
	{
		if (element.ActualWidth <= 0 || element.ActualHeight <= 0)
		{
			throw new InvalidOperationException("Элемент еще не отображен на экране");
		}

		var image = new RenderTargetBitmap(
			(int)element.ActualWidth,
			(int)element.ActualHeight,
			96,
			96,
			PixelFormats.Pbgra32);

		image.Render(element);

		var encoder = new PngBitmapEncoder();
		encoder.Frames.Add(BitmapFrame.Create(image));

		using var file = File.Create(filePath);
		encoder.Save(file);
	}
}
