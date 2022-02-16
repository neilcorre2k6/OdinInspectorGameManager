using System;
using UnityEngine;

#nullable enable

namespace Common {
	public static class Assertion {
		public const string DEFAULT_MESSAGE = "AssertionError";
	
		/**
		 * Asserts the specified expression
		 */
		public static void IsTrue(bool expression, UnityEngine.Object? context = null) {
			IsTrue(expression, DEFAULT_MESSAGE, context);
		}
	
		/**
		 * Asserts the specified expression.
		 */
		public static void IsTrue(bool expression, string assertErrorMessage, UnityEngine.Object? context = null) {
			if (!expression) {
				Debug.LogError(assertErrorMessage, context);
				
#if UNITY_EDITOR
				// Always throw the exception if on editor so we are forced to debug
				throw new Exception(assertErrorMessage);
#endif		
			}
		}
	
		/**
		 * Asserts that the specified pointer is not null.
		 */
		public static void NotNull(object? pointer, string name, UnityEngine.Object? context = null) {
			IsTrue(pointer != null, name, context);
		}
	
		/**
		 * Asserts that the specified pointer is not null.
		 */
		public static void NotNull(object? pointer, UnityEngine.Object? context = null) {
			IsTrue(pointer != null, DEFAULT_MESSAGE, context);
		}
		
		public static unsafe void NotNull(void* address, UnityEngine.Object? context = null) {
			IsTrue(address != null, DEFAULT_MESSAGE, context);
		}
		
		public static void IsSome<T>(Option<T> option, UnityEngine.Object? context = null) {
			IsTrue(option.IsSome, "Option should be Some. Got a None instead.", context);
		}

		public static void IsSome<T>(Option<T> option, string name, UnityEngine.Object? context = null) {
			IsTrue(option.IsSome, name, context);
		}
		
		/**
		 * Asserts that the specified UnityEngine object is not null.
		 */
		public static void NotNull(UnityEngine.Object? pointer, string name, UnityEngine.Object? context = null) {
			if(!pointer) {
				IsTrue(false, name, context);
			}
		}
	
		/**
		 * Asserts that the specified UnityEngine object is not null.
		 */
		public static void NotNull(UnityEngine.Object? pointer, UnityEngine.Object? context = null) {
			if(!pointer) {
				IsTrue(false, DEFAULT_MESSAGE, context);
			}
		}
		
		/**
		 * Asserts that the specified string is not empty.
		 */
		public static void NotEmpty(string? s, string name, UnityEngine.Object? context = null) {
			IsTrue(!string.IsNullOrEmpty(s), name, context);
		}
	
		
		/**
		 * Asserts that the specified string is not empty.
		 */
		public static void NotEmpty(string? s, UnityEngine.Object? context = null) {
			IsTrue(!string.IsNullOrEmpty(s), DEFAULT_MESSAGE, context);
		}	
	}
}

