#include <stdio.h>
#include <stdlib.h>
#include <pthread.h>
#include <semaphore.h>
#include <stdatomic.h>

_Atomic int x = 0, y = 0;
int a = 0, b = 0;
pthread_barrier_t barrier;

void *thread1(void *arg) {
    while(1){
        pthread_barrier_wait(&barrier);
        atomic_store(&x, 1);
        a = y;
        pthread_barrier_wait(&barrier);
    }
}

void *thread2(void *arg) {
    while(1){
        pthread_barrier_wait(&barrier);
        atomic_store(&y, 1);
        b = x;
        pthread_barrier_wait(&barrier);
    }
}

int main() {
    pthread_barrier_init(&barrier, NULL, 3);
    int result[2][2] = { {0, 0}, {0, 0} };

    pthread_t t1, t2;
    pthread_create(&t1, NULL, thread1, NULL);
    pthread_create(&t2, NULL, thread2, NULL);

    int iterations = 1000000;

    struct timespec t_start, t_end;
    clock_gettime(CLOCK_MONOTONIC, &t_start);

    for (int i = 0; i < iterations; i++) {
        x = 0; 
        y = 0;
        a = 0;
        b = 0;
        
        pthread_barrier_wait(&barrier);

        pthread_barrier_wait(&barrier);

        result[a][b]++;
    }

    clock_gettime(CLOCK_MONOTONIC, &t_end);
    double elapsed = (t_end.tv_sec - t_start.tv_sec) * 1e9 + (t_end.tv_nsec - t_start.tv_nsec);

    printf("%d\t%d\t%d\t%d\t%.3f ms\n", result[0][0], result[0][1], result[1][0], result[1][1], elapsed / 1e6);

    return 0;
}