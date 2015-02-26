(ns ^:figwheel-always timbin-fig.core
    (:require [clojure.string :as str]
              [goog.dom :as dom]
              [goog.string :as gstr]
              [goog.string.format]))

(enable-console-print!)

(println "Does this appear in the developers console?")

;; define your app data so that it doesn't get over-written on reload

(defonce app-state (atom {:text "Hello world!"}))

(defn adjust-date [date-id adjust]
  (let [date-control (dom/getElement date-id)
        current-date-text (.-value date-control)
        adj-date (js/Date. (.parse js/Date current-date-text))]
    (adjust adj-date)
    (set! (.-value date-control)
          (gstr/format "%d-%02d-%02d"
                       (.getFullYear adj-date)
                       (inc (.getMonth adj-date))
                       (inc (.getDate adj-date))))))

(defn yesterday! [date]
  (.setDate date (dec (.getDate date))))

(defn prev-day []
  (println "Handling onclick of prevButton")
  (adjust-date "forDate" yesterday!))

(defn tomorrow! [date]
  (.setDate date (inc (.getDate date))))

(defn next-day []
  (println "Handling onclick of nextButton")
  (adjust-date "forDate" tomorrow!))
