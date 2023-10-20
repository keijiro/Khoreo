Khoreo
======

**Khoreo** is an audiovisual project with the Unity game engine and the Roland
MC-101 synthesizer.

![gif](https://i.imgur.com/ffb6ibA.gif)
![gif](https://i.imgur.com/Yko6yiW.gif)

[YouTube video] - 20 minute performance at Channel #22

[YouTube video]: https://www.youtube.com/watch?v=P6Dy3iWpNMc

System Requreiments
-------------------

- Unity 2022 LTS
- Roland MC-101
- UVC compliant video input device (webcam)

How to use
----------

Khoreo uses a MIDI connection to control visual effects from MC-101. You can
use a physical MIDI connection (MIDI interface + MIDI cable) or a USB cable
connection with the [MC-101 MIDI driver].

[MC-101 MIDI driver]: https://www.roland.com/us/products/mc-101/downloads/

You must enable the MIDI message transmission (`TxUSB MIDI`/`TxMIDI Out`) on
all the four tracks. These options are available from the track settings menu
(press `SHIFT` and `TRACK SEL` `1` - `4`). Please see the reference manual for
details.

I designed the visual effects under the assumption that each track is assigned
as follows:

| Track   | Type    | Effects |
| ------- | ------- | ------- |
| Track 1 | Drums 1 | Camera  |
| Track 2 | Drums 2 | Light   |
| Track 3 | Keys    | Hair    |
| Track 4 | Pads    | Others  |

Each effect is only enabled when its assigned audio track is active (not
silent). In other words, you can instantly deactivate the effects by cutting
the track off using the fader.

There are a few key controls that modify the effect parameters.

| Key | Function           |
| --- | ------------------ |
| 1   | Main spotlight     |
| 2   | Posterize effect   |
| Q   | Change hair length |
| A   | Body trails        |
| S   | Web-like string    |
| Z   | Swirling lines     |
| X   | Beanstalk thing    |
