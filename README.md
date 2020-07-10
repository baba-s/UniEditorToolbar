# UniEditorToolbar

再生ボタンの左右に GUI を追加できるエディタ拡張

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

![2020-07-10_101207](https://user-images.githubusercontent.com/6134875/87105403-d48ac000-c295-11ea-9b75-53f609238042.png)

## 補足

* Unity 2019.3.10f1 では Unity を再生するたびに GUI が一瞬消えてしまう現象を確認しております  
    * Enter Play Mode が有効であれば消えません  
