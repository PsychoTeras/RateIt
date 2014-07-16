package com.RateIt.parsers;

import android.text.TextUtils;
import com.RateIt.utils.Logger;
import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public class BaseJsonParser {

    /* ********** Constants                ******* */
    protected final String ErrorCode="ErrorCode";
    protected final String ErrorMessage="ErrorMessage";
    /* ********** Variables                ******* */
    protected final JSONObject json;

    /* ********** Static methods           ******* */

    /* ********** Constructors             ******* */
    public BaseJsonParser(String jsonData) throws JSONException {
        this.json = new JSONObject(jsonData);
    }

    public BaseJsonParser(JSONObject jsonData) throws JSONException {
        this.json = jsonData;
    }

    /* ********** Public instance methods  ******* */
    
    /* ********** Private instance methods ******* */
    protected boolean hasNonEmpty(String field) {
        return hasNonEmpty(json, field);
    }

    protected String getString(String field) {
        return getString(json, field);
    }

    protected String getString(JSONObject json, String field) {
        try {
            return  json.getString(field);
        } catch (JSONException e) {
            return null;
        }
    }

    protected JSONArray getArray(String field) {
        return getArray(json, field);
    }

    protected JSONObject getObject(String field) {
        return getObject(json, field);
    }

    protected JSONObject getObject(JSONObject json, String field) {
        try {
            return json.getJSONObject(field);
        } catch (JSONException e) {
            return null;
        }
    }

    protected JSONObject getFromArray(JSONArray array, int index) {
        try {
            return array.getJSONObject(index);
        } catch (JSONException e) {
            return null;
        }
    }

    protected JSONArray getArray(JSONObject json, String field) {
        try {
            return json.getJSONArray(field);
        } catch (JSONException e) {
            return null;
        }
    }

    protected boolean hasNonEmpty(JSONObject json, String field) {
        return json.has(field) && !json.isNull(field);
    }

    protected int getInt(String field) {
        return getInt(json, field);
    }

    protected int getInt(JSONObject json, String field) {
        return getInt(json, field, 0);
    }

    protected int getInt(JSONObject json, String field, int defaultVal) {
        try{
            if (!json.has(field))
                return defaultVal;

            String string = json.getString(field);
            if(!TextUtils.isEmpty(string)){
                return Integer.parseInt(string);
            }
        }catch(Exception e){
            Logger.e("ApiJsonFunc:getInt:", e);
        }
        return defaultVal;
    }

    protected boolean getBoolean(String field) {
        return getBoolean(json, field);
    }

    protected boolean getBoolean(JSONObject json, String field) {
        try{
            String string = json.getString(field);
            if(!TextUtils.isEmpty(string)){
                return Boolean.parseBoolean(string);
            }
        }catch(Exception e){
            Logger.e("ApiJsonFunc:getBoolean:", e);
        }
        return false;
    }


    protected float getFloat(String field) {
        return getFloat(json, field);
    }

    protected float getFloat(JSONObject json, String field) {
        try{
            if (!json.has(field))
                return 0;

            String string = json.getString(field);
            if(!TextUtils.isEmpty(string)){
                return Float.parseFloat(string);
            }
        }catch(Exception e){
            Logger.e("ApiJsonFunc:getInt:", e);
        }
        return 0;
    }

    protected long getLong(String field) {
        return getLong(json, field);
    }

    protected long getLong(JSONObject json, String field) {
        try{
            String string = json.getString(field);
            if(!TextUtils.isEmpty(string)){
                return Long.parseLong(string);
            }
        }catch(Exception e){
            Logger.e("ApiJsonFunc:getLong:", e);
        }
        return 0;
    }
    
    /* ********** Internal classes         ******* */
}
