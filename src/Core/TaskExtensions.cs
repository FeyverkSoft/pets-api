namespace Core;

using System.Collections.Generic;
using System.Runtime.CompilerServices;

/// <summary>
///     Расширение для работы со задачами
/// </summary>
public static class TaskExtensions
{
    private static async Task<( T1 Task1, T2 Task2)> WhenAll<T1, T2>(Task<T1> t1, Task<T2> t2)
    {
        await Task.WhenAll(t1, t2).ConfigureAwait(false);
        return (Task1: await t1, Task2: await t2);
    }

    public static TaskAwaiter<(T1, T2)> GetAwaiter<T1, T2>(this (Task<T1> Task1, Task<T2> Task2) tasks) =>
        WhenAll(tasks.Task1, tasks.Task2).GetAwaiter();

    private static async Task<(T1, T2, T3)> WhenAll<T1, T2, T3>(Task<T1> t1, Task<T2> t2, Task<T3> t3)
    {
        await Task.WhenAll(t1, t2, t3).ConfigureAwait(false);
        return (await t1, await t2, await t3);
    }

    private static async Task<(T1, T2, T3, T4)> WhenAll<T1, T2, T3, T4>(Task<T1> t1, Task<T2> t2, Task<T3> t3,
        Task<T4> t4)
    {
        await Task.WhenAll(t1, t2, t3, t4).ConfigureAwait(false);
        return (await t1, await t2, await t3, await t4);
    }

    private static async Task<(T1, T2, T3, T4, T5)> WhenAll<T1, T2, T3, T4, T5>(Task<T1> t1, Task<T2> t2,
        Task<T3> t3, Task<T4> t4, Task<T5> t5)
    {
        await Task.WhenAll(t1, t2, t3, t4, t5).ConfigureAwait(false);
        return (await t1, await t2, await t3, await t4, await t5);
    }

    public static TaskAwaiter<(T1, T2, T3)> GetAwaiter<T1, T2, T3>(
        this (Task<T1> Task1, Task<T2> Task2, Task<T3> Task3) tasks) =>
        WhenAll(tasks.Task1, tasks.Task2, tasks.Task3).GetAwaiter();

    public static TaskAwaiter<(T1, T2, T3, T4)> GetAwaiter<T1, T2, T3, T4>(
        this (Task<T1> Task1, Task<T2> Task2, Task<T3> Task3, Task<T4> Task4) tasks) =>
        WhenAll(tasks.Task1, tasks.Task2, tasks.Task3, tasks.Task4).GetAwaiter();

    public static TaskAwaiter<(T1, T2, T3, T4, T5)> GetAwaiter<T1, T2, T3, T4, T5>(
        this (Task<T1> Task1, Task<T2> Task2, Task<T3> Task3, Task<T4> Task4, Task<T5> Task5) tasks) =>
        WhenAll(tasks.Task1, tasks.Task2, tasks.Task3, tasks.Task4, tasks.Task5).GetAwaiter();

    public static ConfiguredTaskAwaitable.ConfiguredTaskAwaiter GetAwaiter(this IEnumerable<Task> tasks) =>
        Task.WhenAll(tasks).ConfigureAwait(false).GetAwaiter();

    public static ConfiguredTaskAwaitable<T1[]>.ConfiguredTaskAwaiter GetAwaiter<T1>(
        this IEnumerable<Task<T1>> tasks) =>
        Task.WhenAll(tasks).ConfigureAwait(false).GetAwaiter();

    private static async Task WhenAll(Task t1, Task t2)
    {
        await Task.WhenAll(t1, t2).ConfigureAwait(false);
    }

    public static TaskAwaiter GetAwaiter(this (Task Task1, Task Task2) tasks) =>
        WhenAll(tasks.Task1, tasks.Task2).GetAwaiter();
}