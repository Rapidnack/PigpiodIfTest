# PigpiodIfTest

Rapidnack.Net/PiGpiodIf.cs is a rewrite of PIGPIO/pigpiod_if2.c in C#, and Rapidnack.Net/XPiGpiodIf.cs is a rewrite of PIGPIO/x_pigpiod_if2.c in C#.  
PigpiodIfTest/PigpiodIfTest.sln is a test application for the Rapidnack.Net/PiGpiodIf.cs.  

![Raspberry Pi and peripherals](http://rapidack.sakura.ne.jp/ttl/wp-content/uploads/2018/09/IMG_20180916_193830-e1537094490777.jpg)

With Rapidnack.Net/PiGpiodIf.cs you can debug on Visual Studio using Raspberry Pi's GPIO.

![debugging on VisualStudio](http://rapidack.sakura.ne.jp/ttl/wp-content/uploads/2018/09/debug1.png)

You can also run the debugged executable file directly on Raspberry Pi.

![run on raspberry pi](http://rapidack.sakura.ne.jp/ttl/wp-content/uploads/2018/09/run1.png)

# Dependency

- Microsoft Visual Studio Community 2015
- .NET Framework 4.5

# Usage

Functions of [pigpiod C Interface](http://abyz.me.uk/rpi/pigpio/pdif2.html) were implemented except for the following functions.
- start_thread()
- stop_thread()
- event_callback()
- event_callback_ex()
- event_callback_cancel()
- wait_for_event()

When adding Rapidnack.Net/PiGpiodIf.cs to your project, please also copy Rapidnack.Net/TcpConnection.cs.  
And please refer to Rapidnack.Net/XPiGpiodIf.cs for how to use each functions.

# Licence

Like pigpio, this software is released under the Unlicense, see LICENSE.

# Authors

[Rapidnack](http://rapidnack.com/)

# References

[http://abyz.me.uk/rpi/pigpio/pdif2.html](http://abyz.me.uk/rpi/pigpio/pdif2.html)  
[abyz.me.uk/rpi/pigpio/pigpio.zip](abyz.me.uk/rpi/pigpio/pigpio.zip)

# Status

Testing pigpiod C# I/F  
pigpio version 64.  
Hardware revision 16.  

Mode/PUD/read/write tests.  
TEST 1.1 PASS (set mode, get mode: 0)  
TEST 1.2 PASS (set pull up down, read: 1)  
TEST 1.3 PASS (set pull up down, read: 0)  
TEST 1.4 PASS (write, get mode: 1)  
TEST 1.5 PASS (read: 0)  
TEST 1.6 PASS (write, read: 1)  

PWM dutycycle/range/frequency tests.  
TEST 2.1 PASS (set PWM range, set/get PWM frequency: 10)  
TEST 2.2 PASS (get PWM dutycycle: 0)  
TEST 2.3 PASS (set PWM dutycycle, callback: 0)  
TEST 2.4 PASS (get PWM dutycycle: 128)  
TEST 2.5 PASS (set PWM dutycycle, callback: 40)  
TEST 2.6 PASS (set/get PWM frequency: 100)  
TEST 2.7 PASS (callback: 400)  
TEST 2.8 PASS (set/get PWM frequency: 1000)  
TEST 2.9 FAILED got 3908 (callback: 4000)  
TEST 2.10 PASS (get PWM range: 255)  
TEST 2.11 PASS (get PWM real range: 200)  
TEST 2.12 PASS (set/get PWM range: 2000)  
TEST 2.13 PASS (get PWM real range: 200)  

PWM/Servo pulse accuracy tests.  
TEST 3.1 PASS (get servo pulsewidth: 500)  
TEST 3.2 PASS (set servo pulsewidth: 40000)  
TEST 3.3 PASS (get servo pulsewidth: 1500)  
TEST 3.4 PASS (set servo pulsewidth: 13333)  
TEST 3.5 PASS (get servo pulsewidth: 2500)  
TEST 3.6 PASS (set servo pulsewidth: 8000)  
TEST 3.7 PASS (set/get PWM frequency: 1000)  
TEST 3.8 PASS (set PWM range: 200)  
TEST 3.9 PASS (get PWM dutycycle: 20)  
TEST 3.10 PASS (set PWM dutycycle: 200)  
TEST 3.11 PASS (get PWM dutycycle: 40)  
TEST 3.12 PASS (set PWM dutycycle: 400)  
TEST 3.13 PASS (get PWM dutycycle: 60)  
TEST 3.14 PASS (set PWM dutycycle: 600)  
TEST 3.15 PASS (get PWM dutycycle: 80)  
TEST 3.16 PASS (set PWM dutycycle: 800)  

Waveforms & serial read/write tests.  
TEST 5.1 PASS (callback, set mode, wave clear: 0)  
TEST 5.2 PASS (pulse, wave add generic: 4)  
TEST 5.3 PASS (wave tx repeat: 9)  
TEST 5.4 PASS (callback: 50)  
TEST 5.5 PASS (wave tx stop: 0)  
TEST 5.6 PASS (serial read open: 0)  
TEST 5.7 FAILED got 3413 (wave clear, wave add serial: 3405)  
TEST 5.8 FAILED got 6827 (wave tx start: 6811)  
TEST 5.9 PASS (callback: 0)  
TEST 5.10 FAILED got 1706 (wave tx busy, callback: 1702)  
TEST 5.11 PASS (wave tx busy, serial read: 0)  
TEST 5.12 PASS (serial read close: 0)  
TEST 5.13 FAILED got 6162314 (wave get micros: 6158148)  
TEST 5.14 PASS (wave get high micros: 6158148)  
TEST 5.15 PASS (wave get max micros: 1800000000)  
TEST 5.16 FAILED got 3413 (wave get pulses: 3405)  
TEST 5.17 FAILED got 3413 (wave get high pulses: 3405)  
TEST 5.18 PASS (wave get max pulses: 12000)  
TEST 5.19 FAILED got 6826 (wave get cbs: 6810)  
TEST 5.20 FAILED got 6826 (wave get high cbs: 6810)  
TEST 5.21 PASS (wave get max cbs: 25016)  

Trigger tests.  
TEST 6.1 FAILED got 4 (gpio trigger count: 5)  
TEST 6.2 PASS (gpio trigger pulse length: 150)  

Watchdog tests.  
TEST 7.1 PASS (set watchdog on count: 39)  
TEST 7.2 PASS (set watchdog off count: 0)  

Bank read/write tests.  
TEST 8.1 PASS (read bank 1: 0)  
TEST 8.2 PASS (read bank 1: 33554432)  
TEST 8.3 PASS (clear bank 1: 0)  
TEST 8.4 PASS (set bank 1: 1)  
TEST 8.5 PASS (read bank 2: 0)  
TEST 8.6 PASS (clear bank 2: 0)  
TEST 8.7 PASS (clear bank 2: -42)  
TEST 8.8 PASS (set bank 2: 0)  
TEST 8.9 PASS (set bank 2: -42)  

Script store/run/status/stop/delete tests.  
TEST 9.1 PASS (store/run script: 100)  
TEST 9.2 PASS (run script/script status: 201)  
TEST 9.3 PASS (run/stop script/script status: 410)  
TEST 9.4 PASS (delete script: 0)  

Serial link tests.  
TEST 10.1 PASS (serial open: 0)  
TEST 10.2 PASS (serial data available: 0)  
TEST 10.3 PASS (serial write: 0)  
TEST 10.4 PASS (serial write byte: 0)  
TEST 10.5 PASS (serial data available: 192)  
TEST 10.6 PASS (serial read: 188)  
TEST 10.7 PASS (serial read: 0)  
TEST 10.8 PASS (serial read byte: 170)  
TEST 10.9 PASS (serial read byte: 85)  
TEST 10.10 PASS (serial read byte: 0)  
TEST 10.11 PASS (serial read byte: 255)  
TEST 10.12 PASS (serial data availabe: 0)  
TEST 10.13 PASS (serial close: 0)  

SMBus / I2C tests.  
TEST 11.1 PASS (i2c open: 0)  
TEST 11.2 PASS (i2c write device: 0)  
TEST 11.3 PASS (i2c read device: 1)  
TEST 11.4 PASS (i2c read device: 229)  
TEST 11.5 PASS (i2c read byte: 229)  
TEST 11.6 PASS (i2c read byte data: 229)  
TEST 11.7 PASS (i2c read byte data: 2)  
TEST 11.8 PASS (i2c write device: 0)  
TEST 11.9 PASS (i2c read device: 13)  
TEST 11.10 PASS (i2c read device: 0)  
TEST 11.11 PASS (i2c write byte data: 0)  
TEST 11.12 PASS (i2c read byte data: 170)  
TEST 11.13 PASS (i2c write byte data: 0)  
TEST 11.14 PASS (i2c read byte data: 85)  
TEST 11.15 PASS (i2c write block data: 0)  
TEST 11.16 PASS (i2c read device: 13)  
TEST 11.17 PASS (i2c read device: 0)  
TEST 11.18 PASS (i2c read i2c block data: 13)  
TEST 11.19 PASS (i2c read i2c block data: 0)  
TEST 11.20 PASS (i2c write i2c block data: 0)  
TEST 11.21 PASS (i2c read i2c block data: 12)  
TEST 11.22 PASS (i2c read i2c block data: 0)  
TEST 11.23 PASS (i2c close: 0)  

SPI tests.  
TEST 12.1 PASS (spi open: 0)  
TEST 12.2 PASS (spi xfer: 3)  
4094  
TEST 12.2 PASS (spi xfer: 3)  
4095  
TEST 12.2 PASS (spi xfer: 3)  
4077  
TEST 12.2 PASS (spi xfer: 3)  
4094  
TEST 12.2 PASS (spi xfer: 3)  
4095  
TEST 12.99 PASS (spi close: 0)  
