﻿
using System;
using System.Collections.Generic;

namespace Unme.Common
{
	// TODO unit test
	public static class FunctionalUtility
	{
		/// <summary>
		/// Memoizes the given function.
		/// </summary>
		public static Func<T> Memoize<T>(Func<T> generator)
		{
			bool hasValue = false;
			T returnValue = default(T);
			return delegate
			{
				if (!hasValue)
				{
					returnValue = generator();
					hasValue = true;
				}

				return returnValue;
			};
		}

		/// <summary>
		/// Memoizes the given function.
		/// </summary>
		public static Func<TInput, TOutput> Memoize<TInput, TOutput>(Func<TInput, TOutput> generator)
		{
			return Memoize<TInput, TInput, TOutput>(generator, input => input);
		}

		/// <summary>
		/// Memoizes the given function.
		/// </summary>
		public static Func<TInput, TOutput> Memoize<TInput, TKey, TOutput>(Func<TInput, TOutput> generator, Func<TInput, TKey> keySelector)
		{
			var cache = new Dictionary<TKey, TOutput>();
			return input =>
			{
				TOutput output;
				if (!cache.TryGetValue(keySelector(input), out output))
				{
					output = generator(input);
					cache.Add(keySelector(input), output);
				}

				return output;
			};
		}
	}
}
