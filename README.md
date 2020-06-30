# UniEditorToolbar

## 使用例

```cs
using Kogane;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
internal static class Example
{
	static Example()
	{
		EditorToolbar.OnLeftGUI  += OnLeftGUI;
		EditorToolbar.OnRightGUI += OnRightGUI;
	}

	private static void OnLeftGUI()
	{
		GUILayout.FlexibleSpace();

		if ( GUILayout.Button( "ピカチュウ", GUILayout.Height( 22 ) ) )
		{
			Debug.Log( "ピカチュウ" );
		}
	}

	private static void OnRightGUI()
	{
		if ( GUILayout.Button( "ピカチュウ", GUILayout.Height( 22 ) ) )
		{
			Debug.Log( "ピカチュウ" );
		}

		GUILayout.FlexibleSpace();
	}
}
```