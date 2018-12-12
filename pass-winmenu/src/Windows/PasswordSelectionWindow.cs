﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;

namespace PassWinmenu.Windows
{
	internal class PasswordSelectionWindow : SelectionWindow
	{
		private readonly List<string> options;

		public PasswordSelectionWindow(IEnumerable<string> options, SelectionWindowConfiguration configuration) : base(configuration)
		{
			this.options = options.ToList();
		}

		protected override void OnContentRendered(EventArgs e)
		{
			base.OnContentRendered(e);
			RedrawLabels(options);
		}

		protected override void SearchBox_OnTextChanged(object sender, TextChangedEventArgs e)
		{
			// We split on spaces to allow the user to quickly search for a certain term, as it allows them
			// to search, for example, for reddit.com/username by entering "re us"
			var terms = SearchBox.Text.ToLower(CultureInfo.CurrentCulture).Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);


			var matching = options.Where((option) =>
			{
				var content = option.ToLower(CultureInfo.CurrentCulture);
				return terms.All(term => content.Contains(term));
			});
			RedrawLabels(matching);
		}

		protected override void HandleSelect()
		{
			if (Selected != null)
			{
				Success = true;
				Close();
			}
		}
	}
}
