set_continuation_bits([Last], [Last]) :- !.
set_continuation_bits([H|T], [ModifiedH|ModifiedT]) :-
    ModifiedH is H \/ 128,
    set_continuation_bits(T, ModifiedT).

extract_chunks(0, Acc, Acc) :- !.
extract_chunks(N, Acc, Chunks) :-
    Chunk is N /\ 127,
    NextN is N >> 7,
    extract_chunks(NextN, [Chunk|Acc], Chunks).

encode_compact(0, [0]) :- !.
encode_compact(N, Result) :-
    extract_chunks(N, [], Chunks),
    set_continuation_bits(Chunks, Result).

main :-
    read_line_to_string(user_input, String),
    statistics(runtime, _), % Сброс таймера
    statistics(memory, [StartMem|_]),
    number_string(N, String),
    encode_compact(N, Result),
    atomic_list_concat(Result, ' ', Output),
    writeln(Output),
    statistics(runtime, [_, ElapsedTime]),
    statistics(memory, [EndMem|_]),
    TimeSec is ElapsedTime / 1000,
    MemMB is (EndMem - StartMem) / 1048576,
    format(user_error, 'Time: ~6f sec~n', [TimeSec]),
    format(user_error, 'Memory: ~6f MB~n', [MemMB]).