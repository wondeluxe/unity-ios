using UnityEditor.iOS.Xcode;

namespace WondeluxeEditor.iOS
{
	/// <summary>
	/// Extension and utility methods for <see href="https://docs.unity3d.com/ScriptReference/iOS.Xcode.PlistElementArray.html">PlistElementArray</see>.
	/// </summary>

	public static class PlistElementArrayExtensions
	{
		/// <summary>
		/// Adds the value of a <see href="https://docs.unity3d.com/ScriptReference/iOS.Xcode.PlistElement.html">PlistElement</see> to a
		/// <see href="https://docs.unity3d.com/ScriptReference/iOS.Xcode.PlistElementArray.html">PlistElementArray</see>.
		/// </summary>
		/// <param name="element">The <see href="https://docs.unity3d.com/ScriptReference/iOS.Xcode.PlistElementArray.html">PlistElementArray</see> to add the value to.</param>
		/// <param name="value">The value to add.</param>

		public static void Add(this PlistElementArray element, PlistElement value)
		{
			if (value is PlistElementBoolean)
			{
				element.AddBoolean(value.AsBoolean());
			}
			else if (value is PlistElementInteger)
			{
				element.AddInteger(value.AsInteger());
			}
			else if (value is PlistElementReal)
			{
				element.AddReal(value.AsReal());
			}
			else if (value is PlistElementDate)
			{
				element.AddDate(value.AsDate());
			}
			else if (value is PlistElementString)
			{
				element.AddString(value.AsString());
			}
			else if (value is PlistElementArray)
			{
				Copy(value.AsArray(), element.AddArray());
			}
			else if (value is PlistElementDict)
			{
				PlistElementDictExtensions.Copy(value.AsDict(), element.AddDict());
			}
		}

		/// <summary>
		/// Deep copies the values from one <see href="https://docs.unity3d.com/ScriptReference/iOS.Xcode.PlistElementArray.html">PlistElementArray</see> to another.
		/// </summary>
		/// <param name="source">The <see href="https://docs.unity3d.com/ScriptReference/iOS.Xcode.PlistElementArray.html">PlistElementArray</see> that contains the values to copy.</param>
		/// <param name="destination">The <see href="https://docs.unity3d.com/ScriptReference/iOS.Xcode.PlistElementArray.html">PlistElementArray</see> that receives the copies.</param>

		public static void Copy(PlistElementArray source, PlistElementArray destination)
		{
			foreach (PlistElement element in source.values)
			{
				destination.Add(element);
			}
		}
	}
}