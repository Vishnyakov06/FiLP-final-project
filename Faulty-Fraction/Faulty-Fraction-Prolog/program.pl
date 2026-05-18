solve(Input, A_Str, B_Str) :-
    split_string(Input, " ", "", [S_Str, C_Str]),
    number_string(C, C_Str),
    string_length(S_Str, Ls),
    string_length(C_Str, Lc),
    La_Estimate is (Ls + Lc) // 2,
    (La = La_Estimate ; La is La_Estimate - 1 ; La is La_Estimate + 1),
    La > 0, La < Ls,
    sub_string(S_Str, 0, La, _, A_Str),
    sub_string(S_Str, La, _, 0, B_Str),
    \+ sub_string(B_Str, 0, 1, _, "0"),
    number_string(A, A_Str),
    number_string(B, B_Str),
    A is B * C.


run_test(T) :-
    statistics(runtime, _),
    statistics(memory, [StartMem|_]),
    
    (solve(T, A, B) -> 
        format("Вход: ~w -> Выход: ~w ~w~n", [T, A, B])
    ;   format("Вход: ~w -> Не найдено~n", [T])),
    
    statistics(runtime, [_, ElapsedTime]),
    statistics(memory, [EndMem|_]),
    TimeSec is ElapsedTime / 1000,
    MemMB is (EndMem - StartMem) / 1048576,
    format(user_error, 'Time: ~6f sec~n', [TimeSec]),
    format(user_error, 'Memory: ~6f MB~n', [MemMB]),
    format(user_error, '-------------------~n', []).



test_all :-
    Tests = ["42 2", "2025225 9", "239239239 1001", "123123 1", "1010 1", "8421 4", 
"400200 2", "10000000001 1000000000", "11 1", "102102 1", "999999999999 1"],
    member(T, Tests),
    run_test(T),
    fail; true.