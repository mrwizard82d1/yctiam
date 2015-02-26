(ns ^:figwheel-always timbin-fig.core
    (:require [clojure.string :as str]
              [goog.dom :as dom]
              [goog.string :as gstr]
              [goog.string.format]))

(enable-console-print!)

(println "Does this appear in the developers console?")

;; define your app data so that it doesn't get over-written on reload

(defonce app-state (atom {:text "Hello world!"}))

(defn prev-day []
  (println "Handling onclick of prevButton")
  (let [date-control (dom/getElement "forDate")
        current-date-text (.-value date-control)
        prev-date (js/Date. (.parse js/Date current-date-text))]
    (println current-date-text)
    (.setDate prev-date (dec (.getDate prev-date)))
    (println "prev" prev-date)
    (println "fullYear" (.getFullYear prev-date))
    (println "month" (inc  (.getMonth prev-date)))
    (println "day" (inc (.getDate prev-date)))
    (set! (.-value date-control)
          (gstr/format "%d-%02d-%02d"
                       (.getFullYear prev-date)
                       (inc (.getMonth prev-date))
                       (inc (.getDate prev-date))))))

(defn next-day []
  (println "Handling onclick of nextButton"))
