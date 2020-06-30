using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Kogane
{
	/// <summary>
	/// Unity エディタのツールバーの部分に独自の GUI を追加できるエディタ拡張
	/// </summary>
	[InitializeOnLoad]
	public static class EditorToolbar
	{
		//================================================================================
		// 定数(static readonly)
		//================================================================================
		private static readonly Type         TOOLBAR_TYPE                   = typeof( EditorGUI ).Assembly.GetType( "UnityEditor.Toolbar" );
		private static readonly FieldInfo    TOOLBAR_GET                    = TOOLBAR_TYPE.GetField( "get" );
		private static readonly Type         GUI_VIEW_TYPE                  = typeof( EditorGUI ).Assembly.GetType( "UnityEditor.GUIView" );
		private static readonly BindingFlags BINDING_ATTR                   = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
		private static readonly PropertyInfo GUI_VIEW_IMGUI_CONTAINER       = GUI_VIEW_TYPE.GetProperty( "imguiContainer", BINDING_ATTR );
		private static readonly FieldInfo    IMGUI_CONTAINER_ON_GUI_HANDLER = typeof( IMGUIContainer ).GetField( "m_OnGUIHandler", BINDING_ATTR );

		//================================================================================
		// イベント
		//================================================================================
		/// <summary>
		/// 再生ボタンの左側に GUI を追加できます
		/// </summary>
		public static event Action OnLeftGUI;

		/// <summary>
		/// 再生ボタンの右側に GUI を追加できます
		/// </summary>
		public static event Action OnRightGUI;

		//================================================================================
		// 関数(static)
		//================================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		static EditorToolbar()
		{
			EditorApplication.update += OnUpdate;
		}

		/// <summary>
		/// ツールバーに独自の GUI を追加するコールバックを登録します
		/// </summary>
		private static void OnUpdate()
		{
			var toolbar = TOOLBAR_GET.GetValue( null );
			if ( toolbar == null ) return;

			EditorApplication.update -= OnUpdate;

			var container = ( IMGUIContainer ) GUI_VIEW_IMGUI_CONTAINER.GetValue( toolbar, null );
			var handler   = ( Action ) IMGUI_CONTAINER_ON_GUI_HANDLER.GetValue( container );

			handler += OnGUI;

			IMGUI_CONTAINER_ON_GUI_HANDLER.SetValue( container, handler );
		}

		/// <summary>
		/// ツールバーに独自の GUI を追加します
		/// </summary>
		private static void OnGUI()
		{
			const float rectY      = 4;
			const float rectHeight = 24;

			var currentViewWidth = EditorGUIUtility.currentViewWidth;

			var leftRect = new Rect
			(
				x: 407,
				y: rectY,
				width: currentViewWidth / 2 - 483,
				height: rectHeight
			);

			var rightRect = new Rect
			(
				x: currentViewWidth / 2 + 32,
				y: rectY,
				width: currentViewWidth / 2 - 428,
				height: rectHeight
			);

			GUILayout.BeginArea( leftRect );
			GUILayout.BeginHorizontal();

			OnLeftGUI?.Invoke();

			GUILayout.EndHorizontal();
			GUILayout.EndArea();

			GUILayout.BeginArea( rightRect );
			GUILayout.BeginHorizontal();

			OnRightGUI?.Invoke();

			GUILayout.EndHorizontal();
			GUILayout.EndArea();
		}
	}
}