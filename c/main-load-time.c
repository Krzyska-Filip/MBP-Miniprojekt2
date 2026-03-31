#include <stdio.h>
#include <stdlib.h>
#include <pthread.h>
#include <semaphore.h>
#include <time.h>
#include <stdatomic.h>

_Atomic int x = 0, y = 0;
int a = 0, b = 0;
pthread_barrier_t barrier;

void *thread1(void *arg) {
    pthread_barrier_wait(&barrier);
    for (int i = 0; i < 1000000; i++) {
        x = 1;
        a = atomic_load(&y);
    }
}

void *thread2(void *arg) {
    pthread_barrier_wait(&barrier);
    for (int i = 0; i < 1000000; i++) {
        y = 1;
        b = atomic_load(&x);
    }
}

int main() {
    pthread_barrier_init(&barrier, NULL, 3);

    pthread_t t1, t2;
    pthread_create(&t1, NULL, thread1, NULL);
    pthread_create(&t2, NULL, thread2, NULL);

    struct timespec t_start, t_end;
    clock_gettime(CLOCK_MONOTONIC, &t_start); 
    pthread_barrier_wait(&barrier);

    pthread_join(t1, NULL);
    pthread_join(t2, NULL);

    clock_gettime(CLOCK_MONOTONIC, &t_end);
    double elapsed = (t_end.tv_sec - t_start.tv_sec) * 1e9 + (t_end.tv_nsec - t_start.tv_nsec);

    printf("%.2f ms\n", elapsed / 1e6);

    return 0;
}