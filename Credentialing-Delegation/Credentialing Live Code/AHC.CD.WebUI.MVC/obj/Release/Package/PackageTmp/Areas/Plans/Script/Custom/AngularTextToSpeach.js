﻿angular.module('AngularSpeach', []).
    factory('speech',["$window", function ($window) {

        if ($window.speechSynthesis) {
            var msg = new SpeechSynthesisUtterance();
        }

        function getVoices() {

            $window.speechSynthesis.getVoices();
            return $window.speechSynthesis.getVoices();
        }

        function sayIt(text, config) {
            var voices = getVoices();

            //choose voice. Fallback to default
            msg.voice = config && config.voiceIndex ? voices[config.voiceIndex] : voices[0];
            msg.volume = config && config.volume ? config.volume : 1;
            msg.rate = config && config.rate ? config.rate : 1;
            msg.pitch = config && config.pitch ? config.pitch : 1;

            //message for speech
            msg.text = text;

            speechSynthesis.speak(msg);
        }


        return {
            sayText: sayIt,
            getVoices: getVoices
        };
    }]);