﻿using SnapPoet.Clients;
using SnapPoet.Services.Interfaces;
using System.Text;

namespace SnapPoet.Services;

public class ImageProcessorService : IImageProcessorService
{
	private readonly ComputerVisionClient _computerVisionClient;

	public ImageProcessorService(
		ComputerVisionClient computerVisionClient)
	{
		_computerVisionClient = computerVisionClient;
	}

	public async Task<string> GetImageDescriptionAsync(MemoryStream imageStream)
	{
		var imageCaptions = await _computerVisionClient.GetImageDenseCaptions(imageStream);

		StringBuilder imageDescription = new();
		if (imageCaptions?.DenseCaptionsResult is not null)
		{
			foreach (var value in imageCaptions.DenseCaptionsResult.Values)
				imageDescription.AppendLine($"{value.Text}");
		}
		
		return imageDescription.ToString().Trim();
	}
}

