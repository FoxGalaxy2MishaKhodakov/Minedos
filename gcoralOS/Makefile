# Получить имя текущей папки (имя проекта)
PROJECT_NAME := $(notdir $(CURDIR))

all: kernel

kernel: other/kasm.o other/kc.o
	ld -m elf_i386 -T other/link.ld -o iso/boot/kernel.bin $^
	grub-mkrescue -o iso/ready.iso $(CURDIR)/iso

other/kasm.o: kernels/kernel.asm
	nasm -f elf32 $< -o $@

other/kc.o: kernels/kernel.c
	gcc -m32 -c $< -o $@

clean:
	rm -f iso/boot/kernel.bin iso/ready.iso other/kasm.o other/kc.o
