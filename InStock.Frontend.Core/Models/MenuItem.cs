using System.Windows.Input;

namespace InStock.Frontend.Core.Models
{
	public class MenuItem
	{
		public string Title { get; }
		public string Subtitle { get; }
		public ICommand Command { get; }

		public MenuItem(string title, string subtitle, ICommand command)
		{
			this.Title = title;
			this.Subtitle = subtitle;
			this.Command = command;
		}
	}
}

