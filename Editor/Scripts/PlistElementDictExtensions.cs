using UnityEditor.iOS.Xcode;
using System.Collections.Generic;

namespace WondeluxeEditor.iOS
{
	/// <summary>
	/// Extension and utility methods for <see href="https://docs.unity3d.com/ScriptReference/iOS.Xcode.PlistElementDict.html">PlistElementDict</see>.
	/// </summary>

	public static class PlistElementDictExtensions
	{
		/// <summary>
		/// Sets the value of a <see href="https://docs.unity3d.com/ScriptReference/iOS.Xcode.PlistElement.html">PlistElement</see> on a
		/// <see href="https://docs.unity3d.com/ScriptReference/iOS.Xcode.PlistElementDict.html">PlistElementDict</see> property.
		/// </summary>
		/// <param name="element">The <see href="https://docs.unity3d.com/ScriptReference/iOS.Xcode.PlistElementDict.html">PlistElementDict</see> to set the value on.</param>
		/// <param name="key">The key of the property.</param>
		/// <param name="value">The value of the property.</param>

		public static void Set(this PlistElementDict element, string key, PlistElement value)
		{
			if (value is PlistElementBoolean)
			{
				element.SetBoolean(key, value.AsBoolean());
			}
			else if (value is PlistElementInteger)
			{
				element.SetInteger(key, value.AsInteger());
			}
			else if (value is PlistElementReal)
			{
				element.SetReal(key, value.AsReal());
			}
			else if (value is PlistElementDate)
			{
				element.SetDate(key, value.AsDate());
			}
			else if (value is PlistElementString)
			{
				element.SetString(key, value.AsString());
			}
			else if (value is PlistElementArray)
			{
				PlistElementArray array = element[key] as PlistElementArray;

				if (array == null)
				{
					array = element.CreateArray(key);
				}

				PlistElementArrayExtensions.Copy(value.AsArray(), array);
			}
			else if (value is PlistElementDict)
			{
				PlistElementDict dictionary = element[key] as PlistElementDict;

				if (dictionary == null)
				{
					dictionary = element.CreateDict(key);
				}

				Copy(value.AsDict(), dictionary);
			}
		}

		/// <summary>
		/// Deep copies key/value pairs from one <see href="https://docs.unity3d.com/ScriptReference/iOS.Xcode.PlistElementDict.html">PlistElementDict</see> to another.
		/// </summary>
		/// <param name="source">The <see href="https://docs.unity3d.com/ScriptReference/iOS.Xcode.PlistElementDict.html">PlistElementDict</see> that contains the key/value pairs to copy.</param>
		/// <param name="destination">The <see href="https://docs.unity3d.com/ScriptReference/iOS.Xcode.PlistElementDict.html">PlistElementDict</see> that receives the copies.</param>

		public static void Copy(PlistElementDict source, PlistElementDict destination)
		{
			foreach (KeyValuePair<string, PlistElement> element in source.values)
			{
				destination.Set(element.Key, element.Value);
			}
		}
	}
}