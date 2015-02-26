(ns timbin-cljsb.core
  (:require [goog.dom :as dom]))

(enable-console-print!)

(println "Does this appear in the developers console?")

(defn prev-day []
  (println "Handling onclick of prevButton")
  (let [date-control (dom/getElement "forDate")
        current-date (.-value date-control)]
    (println current-date)
    (set! (.-value date-control)
          (.setDate current-date (dec (.getDate current-date))))))

(defn next-day []
  (println "Handling onclick of nextButton"))
