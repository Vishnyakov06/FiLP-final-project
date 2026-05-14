:- initialization(main, main).

main :-
    catch(run, _, true).

run :-
    statistics(runtime, [StartTime, _]),
    statistics(heapused, StartHeap),

    read_line_to_string(user_input, Line0),
    number_string(N, Line0),
    read_boxes(N, Boxes),
    reduce_mins(Boxes, [MinA, MinB, MinC]),
    Volume is MinA * MinB * MinC,
    write(Volume), nl,

    statistics(runtime, [EndTime, _]),
    statistics(heapused, EndHeap),
    TimeMs is EndTime - StartTime,
    TimeSec is TimeMs / 1000.0,
    MemBytes is EndHeap - StartHeap,
    MemMB is MemBytes / (1024.0 * 1024.0),
    format("Time: ~4f sec~n", [TimeSec]),
    format("Memory: ~4f MB~n", [MemMB]).

read_boxes(0, []) :- !.
read_boxes(N, [Sorted | Rest]) :-
    N > 0,
    read_line_to_string(user_input, Line),
    split_string(Line, " ", " ", Parts),
    maplist(number_string, Dims, Parts),
    msort(Dims, Sorted),
    N1 is N - 1,
    read_boxes(N1, Rest).

reduce_mins([Box], Box) :- !.
reduce_mins([Box | Rest], Result) :-
    reduce_mins(Rest, TailMin),
    pairwise_min(Box, TailMin, Result).

pairwise_min([A1,B1,C1], [A2,B2,C2], [MinA,MinB,MinC]) :-
    MinA is min(A1, A2),
    MinB is min(B1, B2),
    MinC is min(C1, C2).